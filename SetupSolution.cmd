IF NOT EXIST Shippable mkdir shippable
IF NOT EXIST Shippable\testresults mkdir Shippable\testresults
IF NOT EXIST Shippable\codecoverage mkdir Shippable\codecoverage
.nuget\nuget.exe install .nuget\packages.config -Output packages