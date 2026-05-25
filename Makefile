MSBUILD:=msbuild

debug:
	$(MSBUILD) TholinsPqsAdditions.csproj /p:Configuration=Debug /p:Platform=x64
	cp obj/x64/Debug/TholinsPQSAdditions.dll .

release:
	$(MSBUILD) TholinsPqsAdditions.csproj /p:Configuration=Release /p:Platform=x64
	cp obj/x64/Release/TholinsPQSAdditions.dll .

clean:
	rm -f TholinsPQSAdditions.dll
	rm -rf obj

.PHONY: release
