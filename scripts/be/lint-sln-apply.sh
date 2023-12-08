#!/bin/sh

[ -z $linter_verbosity ] && linter_verbosity="diagnostic"
[ -z $linter_severity ] && linter_severity="error"
[ -z $solution_folder ] && solution_folder="./src"
[ -z $update_tool ] && update_tool="false"
[ -z $err_code ] && err_code=100

__exit_error() {
  exit $err_code
}

[ "$update_tool" = "true" ] && (dotnet tool update --global dotnet-format || __exit_error)

dotnet format $solution_folder \
  --verbosity ${linter_verbosity} \
  --severity ${linter_severity}
