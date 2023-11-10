DEPLOY="./Deployment/azure/zip.sh"
LOGS="./Deployment/azure/logs.sh"
APP_SERVICE="./Deployment/Azure/app_service.sh"

db:
	psql tailwind < "./Data/db.sql" -q

test: db
	dotnet test

zip:
	source $(DEPLOY)

logs:
	source $(LOGS)

app_service:
	source $(APP_SERVICE)

.phony: zip app_service logs