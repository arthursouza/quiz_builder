1 - Should the jwt secret in appsettings be a secret? Stored in user secrets.
> It could be a development secret, while the production secret would be stored in azure keyvault, aws secrets, or a similar keystore.

2 - Where is the connection string?
> In development, stored in user secret. Developers can have different connection strings locally, so avoiding them being commited.

