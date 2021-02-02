openapi-generator generate -c autogen-config/csharp-server.json -g aspnetcore -i oas/game-engine.v1.json -o server
find server -name *.cs ! -type d -exec bash -c 'unexpand -t 4 --first-only "$0" > /tmp/e && mv /tmp/e "$0"' {} \;
