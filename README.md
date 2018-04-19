# The Super Sankey

Those microservices were built during StatCan Energy Hackathon of 2018.

## How to process the data

### Requirements

- SAS EG

### Step

git clone https://github.com/FrancisGrignon/TheSuperSankey.git
cd TheSuperSankey/src

- Download StatCan Cansim Data into the TheSuperSankey/src/directory

http://www5.statcan.gc.ca/access_acces/alternative_alternatif?l=eng&keng=827.4&kfra=827.4&teng=Download%20file%20from%20CANSIM&tfra=Fichier%20extrait%20de%20CANSIM&loc=http://www20.statcan.gc.ca/tables-tableaux/cansim/csv/01280016-eng.zip&dispext=CSV

- Open SAS EG
- Open both of .egp files
-   Run the "Import" program

## How to build the web sites

### Requirement

- Docker

### Step

git clone https://github.com/FrancisGrignon/TheSuperSankey.git
cd TheSuperSankey/src
.\build.ps1
.\run.ps1

Open your browser on http://localhost

## Future improvement

Add a container to do the data processing.

## Team Members

Frank Beaupré
Francis Grignon
Jean-Sébastien Godin
Kristin Loiselle-Lapointe
Dimitri Tennikov

## References

StatCan Cansim Table 128-0016 Supply and demand of primary and secondary energy in terajoules
http://www5.statcan.gc.ca/cansim/a05?lang=eng&id=1280016