build-compose:
	docker compose -f ./.docker/docker-compose.yaml -p foodie up
remove-compose:
	docker compose -f ./.docker/docker-compose.yaml -p foodie down
remove-images:
	docker rmi foodie-api foodie-proxy
publish:
	dotnet publish
run:
	dotnet watch run