#!/bin/sh

[ -z $linter_verbosity ] && linter_verbosity="diagnostic"
[ -z $linter_severity ] && linter_severity="error"
[ -z $solution_folder ] && solution_folder="./src"
[ -z $update_tool ] && update_tool="false"
[ -z $linter_report ] && linter_report="./linter/report.json"
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

__init() {
    set -e

    local lr=$(realpath ${linter_report})
    (realpath ${linter_report} | grep -Eq "^$(realpath .)(.*)") || __exit_error

    rm -f "${linter_report}"
    mkdir -p $(dirname $linter_report)

    [ "$update_tool" = "true" ] && (dotnet tool update --global dotnet-format || __exit_error)

    set +e
}

__init

dotnet format $solution_folder \
    --verify-no-changes \
    --verbosity ${linter_verbosity} \
    --severity ${linter_severity} \
    --report $(realpath ${linter_report})

__print_result

[ -f "${linter_report}" ] || __exit_error
