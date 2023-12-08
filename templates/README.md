# Generation from Templates

Use [cog](https://github.com/filariow/cog/releases/latest).

It's a single binary, put it into a folder in the $PATH (or %PATH% in Windows) and you will be able to use it.

> Note
> On Windows you may need to rename the file from `cog` to `cog.exe`

## Demo - Generate a CRUD entity

```sh
cd templates/crud-entity
# the following command will put files directly into the src folder
# ensure to version everything using a git commit before executing it.
cog --ext "tmpl" --outdir "../../src"
```
