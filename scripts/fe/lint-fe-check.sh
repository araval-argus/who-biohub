#!/bin/sh

[ -z $solution_folder ] && solution_folder="./src/fe"
[ -z $update_tool ] && update_tool="false"
[ -z $err_code ] && err_code=100

__exit_error() {
    exit $err_code
}

__print_result() {
    local rc=$?
    echo ""
    echo "****************************************************************"
    echo -n "Linter validation "
    [ $rc = 0 ] || echo -n "NOT "
    echo "PASSED"
    echo "****************************************************************"
    exit $rc
}

[ "$update_tool" = "true" ] && (npm install --location=global eslint || __exit_error)

eslint --ext '.ts,.vue' "$solution_folder"

__print_result
