# Platformus 2.1.0 Sample Mobile App Admin Panel

[![Join the chat at https://gitter.im/Platformus/Platformus](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/Platformus/Platformus?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

## Introduction

[Platformus](https://github.com/Platformus/Platformus) is free, open source, and cross-platform developer-friendly CMS
based on ASP.NET Core, [ExtCore framework](https://github.com/ExtCore/ExtCore),
and [Magicalizer](https://github.com/Magicalizer/Magicalizer).

This project can be used as a base for creating admin panels for mobile apps. It contains backend UI (categories and products sample),
users, roles, permissions, access/refresh tokens (JWT), configurations, API with complex validation, filtering, sorting, and paging etc.

### How To Try

1. Build and run the web application.
2. Get access token for default user.

POST: /en/v1/access-tokens

```
{"username": "admin@platformus.net", "password": "admin"}
```

3. Get categories or products (do not forget to put JWT from the step 2 in the request headers).

GET: /en/v1/categories?fields=name.localizations

GET: /en/v1/categories?name.value.contains=izza&fields=name.localizations

GET: /en/v1/products?fields=name.localizations

GET: /en/v1/products?fields=name.localizations,category.name.localizations

GET: /en/v1/products?category.name.value.contains=izza&fields=name.localizations,category.name.localizations

You can get rid of the culture code in the URL in the configurations (navigate to '/backend').

## Links

Live demo: http://mobile-app-admin-panel-demo.platformus.net/

Sources on GitHub: https://github.com/Platformus/Platformus

Website: http://platformus.net/

Docs: http://docs.platformus.net/

Author: http://sikorsky.pro/

Patreon: https://www.patreon.com/dmitry_sikorsky (you can support this project)
