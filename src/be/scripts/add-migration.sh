#!/bin/bash

function usage() {
    echo "usage: ${BASH_SOURCE[0]} NAME_OF_MIGRATION"
    exit 1
}

[ -z $1 ] && usage

bp=$(realpath "$(dirname ${BASH_SOURCE[0]})/..")
src="$bp"
sql_pwd="Sup3rsecret"
ctr_name="who_biohub_db_migration"
SQLCONNSTR_DefaultConnection="Server=localhost,14333;Initial Catalog=iot;Persist Security Info=False;User ID=sa;Password=${sql_pwd};TrustServerCertificate=True;Connection Timeout=30;"
mig_root="${src}/src/WHO.BioHub.DAL.Migrator"
mig_appsettings="${mig_root}/appsettings.json"
dal_root="${src}/src/WHO.BioHub.DAL"
db_img="mcr.microsoft.com/mssql/server:2019-latest"

## spin up database
docker run \
    --rm \
    -e 'ACCEPT_EULA=Y' \
    -e "SA_PASSWORD=${sql_pwd}" \
    -p 14333:1433 \
    --name "${ctr_name}" \
    -d \
    "${db_img}" >/dev/null

## generate temporary appsettings
cp "${mig_appsettings}" "${mig_appsettings}.bkp"
echo '{"ConnectionStrings":{"SQLServer":"${SQLCONNSTR_DefaultConnection}"}}' |
    SQLCONNSTR_DefaultConnection=${SQLCONNSTR_DefaultConnection} envsubst >"${mig_appsettings}"

## generate migrations
dotnet ef migrations add "$1" \
    --project "${dal_root}" \
    --startup-project "${mig_root}"

## cleanup
docker stop "${ctr_name}" >/dev/null
cp "${mig_appsettings}.bkp" "${mig_appsettings}"
rm -f "${mig_appsettings}.bkp"
