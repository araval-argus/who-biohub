#!/bin/sh
: "
This script loads the Provisioner Service Principal's certificate as secret
into a given pipeline
"

set -e

[ -z "$PIPELINE_ID" ] && echo "PIPELINE_ID is required" && exit 1
[ -z "$ORGANIZATION_URL" ] && echo "ORGANIZATION_URL is required" && exit 1
[ -z "$PROJECT" ] && echo "PROJECT is required" && exit 1

CERT_NAME="who-biohub-provisioner-auth"
KV_NAME="kv-who-biohub-tf"
WORKDIR="/tmp/wb-ado"

function cleanup {
	rm -rf "$WORKDIR"
}

mkdir "$WORKDIR"
trap cleanup EXIT

az keyvault secret download \
    --file "$WORKDIR/cert.pfx" \
    --encoding "base64" \
    --vault-name ${KV_NAME} \
    --name ${CERT_NAME}

openssl pkcs12 -in "$WORKDIR/cert.pfx" -passin pass: -out "$WORKDIR/cert.pem" -nodes

cat "$WORKDIR/cert.pem" | base64 -w 0 >"$WORKDIR/cert.pem.b64"

var_name=servicePrincipalCertificatePEM

az pipelines variable delete \
	--organization="$ORGANIZATION_URL" \
	--name "$var_name" \
	--pipeline-id "$PIPELINE_ID" \
	--project "$PROJECT" \
	--yes || true

AZURE_DEVOPS_EXT_PIPELINE_VAR_servicePrincipalCertificatePEM=$(cat "$WORKDIR/cert.pem.b64") \
	az pipelines variable create \
	--organization="$ORGANIZATION_URL" \
	--name "$var_name" \
	--pipeline-id "$PIPELINE_ID" \
	--project "$PROJECT" \
	--secret true
