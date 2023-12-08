.PHONY: fe-fmt fe-fmt-check fe-update-tool be-fmt be-fmt-check be-update-tool

# Back-end

be-update-tool:
	dotnet tool update --global dotnet-format

be-fmt-check:
	dotnet format src --verbosity diagnostic --severity error --verify-no-changes

be-fmt:
	dotnet format src --verbosity diagnostic --severity error

be-fmt-docker:
	docker run -v "$(CURDIR):/app" --rm -w /app mcr.microsoft.com/dotnet/sdk:6.0 bash -c "dotnet tool update --global dotnet-format && dotnet format src --verbosity diagnostic --severity error"

# Front-end

fe-update-tool:
	npm install --location=global eslint

fe-fmt-check:
	eslint --ext '.ts,.vue' "src/fe/"

fe-fmt:
	eslint --fix --ext '.ts,.vue' "src/fe/"
