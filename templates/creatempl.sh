#!/bin/sh

function __mv() {
    fn=$(dirname $1 | sed -e 's/Laboratories/{{.Entity.Plural}}/g' | sed -e 's/Laboratory/{{.Entity.Singular}}/g' | sed -e 's/^\.\//.\/out\//g')
    n=$(echo $1 | sed -e 's/Laboratories/{{.Entity.Plural}}/g' | sed -e 's/Laboratory/{{.Entity.Singular}}/g' | sed -e 's/^\.\//.\/out\//g')
    echo "from $1 to $n"
    mkdir -p $"fn"
    cp "$1" "$n"
}

export -f __mv
find ./$1 -type f -iname "*.tmpl" | xargs -I{} sh -c '__mv "$@"' _ {}
find ./out -type f -iname "*.tmpl" -exec sed -i -e "s/Laboratories/{{.Entity.Plural}}/g; s/Laboratory/{{.Entity.Singular}}/g; s/DataManagement/{{.Module.Name}}/g" {} \;
