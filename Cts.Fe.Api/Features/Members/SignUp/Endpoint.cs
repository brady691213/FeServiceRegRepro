namespace Cts.Fe.Features.Members.SignUp;

sealed class Endpoint : Endpoint<Request,Response, Mapper>
{
    public override void Configure()
    {
        Post("members/signup");
        PreProcessor<DuplicateInfoChecker>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        var member = Map.ToEntity(r);

        member.MemberNumber = await DB.NextSequentialNumberAsync<Member>(c) + 100;
        member.SignupDate = DateOnly.FromDateTime(DateTime.UtcNow);

        await member.SaveAsync(cancellation: c);
        
        await SendAsync(
            new()
            {
                MemberId = member.ID,
                MemberNumber = member.MemberNumber
            });
    }
}