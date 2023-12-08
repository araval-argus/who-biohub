## Demo
.PHONY: run-demo
run-demo:
	docker compose \
		-f docker-compose.yml \
		-f docker-compose.demo.yml \
		up --build

.PHONY: run-demo-detached
run-demo-detached:
	docker compose \
		-f docker-compose.yml \
		-f docker-compose.demo.yml \
		up -d --build

.PHONY: clean-demo
clean-demo:
	docker compose \
		-f docker-compose.yml \
		-f docker-compose.demo.yml \
		down

## Dev Infra
.PHONY: run-dev-infra
run-dev-infra:
	docker compose \
		-f docker-compose.yml \
		-f docker-compose.dev.yml \
		up --build

.PHONY: run-dev-infra-detached
run-dev-infra-detached:
	docker compose \
		-f docker-compose.yml \
		-f docker-compose.dev.yml \
		up -d --build

.PHONY: clean-dev-infra
clean-dev-infra:
	docker compose \
		-f docker-compose.yml \
		-f docker-compose.dev.yml \
		down

## Dev Data Management function
.PHONY: run-dev-funcdata
run-dev-funcdata:
	cd "be/src/DataManagement/WHO.BioHub.DataManagement.FunctionApp" && \
	ConnectionStrings__SQLServer="Server=localhost,14330;Database=biohub;User ID=sa;Password=Sup3rsecret;TrustServerCertificate=True" \
	OpenApi__HostNames="http://localhost:12081/api" \
		func start --csharp --port 12081

## Dev Public Data function
.PHONY: run-dev-funcpublicdata
run-dev-funcpublicdata:
	cd "be/src/PublicData/WHO.BioHub.PublicData.FunctionApp" && \
	ConnectionStrings__SQLServer="Server=localhost,14330;Database=biohub;User ID=sa;Password=Sup3rsecret;TrustServerCertificate=True" \
	OpenApi__HostNames="http://localhost:12084/api" \
		func start --csharp --port 12084

## Dev Document Management function
.PHONY: run-dev-funcdocument
run-dev-funcdocument:
	cd "be/src/DocumentManagement/WHO.BioHub.DocumentManagement.FunctionApp" && \
	ConnectionStrings__SQLServer="Server=localhost,14330;Database=biohub;User ID=sa;Password=Sup3rsecret;TrustServerCertificate=True" \
	OpenApi__HostNames="http://localhost:12082/api" \
		func start --csharp --port 12082

## Dev Search function
.PHONY: run-dev-funcsearch
run-dev-funcsearch:
	cd "be/src/Search/WHO.BioHub.Search.FunctionApp" && \
	ConnectionStrings__SQLServer="Server=localhost,14330;Database=biohub;User ID=sa;Password=Sup3rsecret;TrustServerCertificate=True" \
	OpenApi__HostNames="http://localhost:12083/api" \
		func start --csharp --port 12083

## Dev Front-end
.PHONY: run-dev-fe
run-dev-fe:
	cd "fe/" && \
	VUE_APP_API_DATA_URL="http://localhost:12000/api/data" \
	VUE_APP_API_PUBLICDATA_URL="http://localhost:12000/api/publicdata" \
	VUE_APP_API_BE_DATA_URL="http://localhost:12081/api/" \
	VUE_APP_API_BE_PUBLICDATA_URL="http://localhost:12084/api/" \
	VUE_APP_API_DOCUMENT_URL="http://localhost:12000/api/document" \
	VUE_APP_API_BE_DOCUMENT_URL="http://localhost:12082/api/" \
	PORT=12000 \
	VUE_APP_PROXY_PORT=12000 \
		npm run serve 
