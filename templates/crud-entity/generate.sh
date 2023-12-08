#!/bin/sh

set -xe


[ -z "$COG_ENTITY_PLURAL" ] && echo "[WRN] Environment variable COG_ENTITY_PLURAL not set, you are using defaults from cog.yaml file" >&2
[ -z "$COG_ENTITY_SINGULAR" ] && echo "[WRN] Environment variable COG_ENTITY_SINGULAR not set, you are using defaults from cog.yaml file" >&2
[ -z "$COG_MODULE_NAME" ] && echo "[WRN] Environment variable COG_MODULE_NAME not set, you are using defaults from cog.yaml file" >&2

bp=$(realpath "$(dirname ${BASH_SOURCE[0]})/../..")

eval "cog \
  -e tmpl \
  --outdir $bp \
  $([ ! -z "$COG_ENTITY_PLURAL" ] && echo "--set=Entity.Plural=$COG_ENTITY_PLURAL") \
  $([ ! -z "$COG_ENTITY_SINGULAR" ] && echo "--set=Entity.Singular=$COG_ENTITY_SINGULAR") \
  $([ ! -z "$COG_MODULE_NAME" ] && echo "--set=Module.Name=$COG_MODULE_NAME") \
  ."
