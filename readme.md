1 - Should the jwt secret in appsettings be a secret? Stored in user secrets.
> It could be a development secret, while the production secret would be stored in azure keyvault, aws secrets, or a similar keystore.

2 - Where is the connection string?
> In development, stored in user secret. Developers can have different connection strings locally, so avoiding them being commited.

3 - The IdentityUser table has no FKs pointing to it. If you need a query that needs to bring quizzes with the creators names, how to go about it?
> In case we come to a situation where users must have public data, we can create a new table to store public user information, and associate this table with the Quizzes. This public user data table should contain the identity user id.