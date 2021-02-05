sed -i s/CLASS/$1/g "./server/.openapi-generator-ignore"
./autogen-server.sh
sed -i s/$1/CLASS/g "./server/.openapi-generator-ignore"