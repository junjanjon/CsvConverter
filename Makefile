
run:
	dotnet run

format:
	dotnet tool restore
	dotnet format

test:
	./scripts/ci_test.sh
