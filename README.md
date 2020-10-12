# herokufy-more

![Actions Status](https://github.com/jeremymaya/herokufy-more/workflows/deploy/badge.svg)

Author: Kyungrae Kim

Endpoint: <https://herokufy-more.herokuapp.com>

---

## Description

This is a further proof of concept that an ASP.NET web application with __multiple__ relational databases can be continuously integrated and deployed to Heroku by combining the power of [Docker](https://www.docker.com) and [GitHub Actions](https://github.com/features/actions).

This repository showcases a more complicated ASP.NET web application deployment scenario compared to [Herokufy Dotnet](https://github.com/jeremymaya/herokufy-dotnet) by adding a second database seeded with a Admin account using ASP.NET Identity.

The below is the Admin account login information for demo purpose:

```text
Email: admin@email.com
Password: ReallyStrongPassword1234!
```

---

## What Differs

Pay close attention to `Dockerfile` and `deployment.yml`!

### Dockerfile

The variable defined as `ARG` in Dockerfile is only available during the build-time. In order to access other variables throughout the application, those variables need to be defined as `ENV`. However, Heroku does NOT support the `--env` option. Therefore, we need to define an `ENV` variable that references an `ARG`.

For example:

```dockerfile
ARG ADMIN_EMAIL
ARG ADMIN_PASSWORD
ENV ADMIN_EMAIL_ENV=$ADMIN_EMAIL
ENV ADMIN_PASSWORD_ENV=$ADMIN_PASSWORD
```

### Workflow

In order to pass multiple variables from GitHub Secrets, separate each variables with a `,`.

```yaml
--arg ADMIN_EMAIL=${{ secrets.ADMIN_EMAIL }},ADMIN_PASSWORD=${{ secrets.ADMIN_PASSWORD }}
```

---

## Credits

* [Heroku - Heroku CLI Commands](https://devcenter.heroku.com/articles/heroku-cli-commands)
* [vsupalov - Docker ARG vs ENV](https://vsupalov.com/docker-arg-vs-env/)
