public class JWTMiddleware
{
    private readonly RequestDelegate _next;

    public JWTMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check if the Authorization header is present
        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();

            // Extract the token (remove 'Bearer ' prefix if present)
            if (authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length);
                // Log or inspect the token
                Console.WriteLine($"JWT Token: {token}");

                // (Optional) Parse the token to read claims
                try
                {
                    token = token.Substring(7);
                    var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);

                    // Log claims
                    foreach (var claim in jwtToken.Claims)
                    {
                        Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to parse token: {ex.Message}");
                }
                Console.WriteLine("time now: " + DateTime.Now.AddHours(2));
            }
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }
}
