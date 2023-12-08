#!/bin/sh

module="DataManagement"
bp=$(realpath "$(dirname ${BASH_SOURCE[0]})/../..")
usecases_folder="$bp/src/be/src/$module/WHO.BioHub.$module.Core/UseCases"


for i in $usecases_folder/* ; do
  if [ -d "$i" ]; then
    entity_plural="${i##*/}"
    entity_singular=$(ls $i | grep "Create" | sed 's/^Create//;s/\.cs$//;')

    cog \
      -e tmpl \
      --outdir $bp \
      --set Entity.Plural=$entity_plural \
      --set Entity.Singular=$entity_singular \
      --set Module.Name=$module \
      .
  fi
done