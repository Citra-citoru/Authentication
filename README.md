

### Features

### Directory Layout

```shell
.
├── /.vscode/                   # Visual Studio Code settings
├── /build/                     # The folder for compiled output
├── /controllers/               # routes for the API
├── /data/						# context and dependency injection
│── /migrations/				# scripts for migrate the data
│── /models/					# entities
│── /properties/				# The list of project dependencies and NPM scripts
```


### Prerequisites

* OS X, Windows or Linux
* [.NET Core](https://www.microsoft.com/net/core) and [.NET Core SDK](https://www.microsoft.com/net/core)
* [Visual Studio Code](https://code.visualstudio.com/)

### Getting Started

**Step 1**. Clone the latest version of the repository:

```shell
$ git clone https://github.com/Citra-citoru/Authentication.git
$ cd AuthApi
```

**Step 2**. Create migration script and sql server database and tables.

```shell
$ add-Migration initial				#create migration script
$ update_database
```
**Step 3**. Build the database

### Swagger

https://localhost:44347/swagger/index.html