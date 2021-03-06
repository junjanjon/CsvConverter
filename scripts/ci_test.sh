#!/bin/bash -xe

mkdir -p TestCase
echo '' > TestCase/1.csv
echo '' > TestCase/1.tsv
echo '1,2,3' > TestCase/2.csv
echo '4,5,6' >> TestCase/2.csv
echo '1	2	3' > TestCase/2.tsv
echo '4	5	6' >> TestCase/2.tsv
echo '1,2,,4' > TestCase/3.csv
echo ',6,7,8' >> TestCase/3.csv
echo '1	2		4' > TestCase/3.tsv
echo '	6	7	8' >> TestCase/3.tsv
mkdir -p tmp
dotnet build -c Release
dotnet ./bin/Release/net6.0/CsvConverter.dll --input TestCase/1.csv --output tmp/1.tsv
dotnet ./bin/Release/net6.0/CsvConverter.dll --input TestCase/2.csv --output tmp/2.tsv
dotnet ./bin/Release/net6.0/CsvConverter.dll --input TestCase/3.csv --output tmp/3.tsv

cmp TestCase/1.tsv tmp/1.tsv -s
cmp TestCase/2.tsv tmp/2.tsv -s
cmp TestCase/3.tsv tmp/3.tsv -s
