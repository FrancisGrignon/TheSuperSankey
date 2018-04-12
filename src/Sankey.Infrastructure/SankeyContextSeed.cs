using Microsoft.Extensions.Logging;
using Sankey.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sankey.Infrastructure
{
    public class SankeyContextSeed
    {
        public async Task SeedAsync(SankeyContext context, string contentRootPath, ILogger<SankeyContextSeed> logger)
        {
            if (false == context.Fuels.Any())
            {
                context.Fuels.AddRange(GetFuelsFromFile(contentRootPath, logger));

                await context.SaveChangesAsync();
            }

            if (false == context.Geos.Any())
            {
                context.Geos.AddRange(GetGeosFromFile(contentRootPath, logger));

                await context.SaveChangesAsync();
            }

            if (false == context.Supplies.Any())
            {
                context.Supplies.AddRange(GetSuppliesFromFile(contentRootPath, logger));

                await context.SaveChangesAsync();
            }

            if (false == context.Flows.Any())
            {
                context.Flows.AddRange(GetFlowsFromFile(contentRootPath, logger, context));

                await context.SaveChangesAsync();
            }
        }

        private IEnumerable<Flow> GetFlowsFromFile(string contentRootPath, ILogger<SankeyContextSeed> logger, SankeyContext context)
        {
            string path = Path.Combine(contentRootPath, "Setup", "Flows.csv");

            if (false == File.Exists(path))
            {
                logger.LogInformation("Flows loaded from preconfigured values.");

                return GetPreconfiguredFlows();
            }

            logger.LogInformation("Flows loaded from file.");

            return File.ReadAllLines(path)
                .Skip(1) // skip header row
                .Select(x => CreateFlow(x, context))
                .Where(x => x != null);
        }
             
        private IEnumerable<Fuel> GetFuelsFromFile(string contentRootPath, ILogger<SankeyContextSeed> logger)
        {
            string path = Path.Combine(contentRootPath, "Setup", "Fuels.csv");

            if (false == File.Exists(path))
            {
                logger.LogInformation("Fuels loaded from preconfigured values.");

                return GetPreconfiguredFuels();
            }

            logger.LogInformation("Fuels loaded from file.");

            return File.ReadAllLines(path)
                .Skip(1) // skip header row
                .Select(x => CreateFuel(x))
                .Where(x => x != null);
        }

        private IEnumerable<Geo> GetGeosFromFile(string contentRootPath, ILogger<SankeyContextSeed> logger)
        {
            string path = Path.Combine(contentRootPath, "Setup", "Geos.csv");

            if (false == File.Exists(path))
            {
                logger.LogInformation("Geos loaded from preconfigured values.");

                return GetPreconfiguredGeos();
            }

            logger.LogInformation("Geos loaded from file.");

            return File.ReadAllLines(path)
                .Skip(1) // skip header row
                .Select(x => CreateGeo(x))
                .Where(x => x != null);
        }

        private IEnumerable<Supply> GetSuppliesFromFile(string contentRootPath, ILogger<SankeyContextSeed> logger)
        {
            string path = Path.Combine(contentRootPath, "Setup", "Supplies.csv");

            if (false == File.Exists(path))
            {
                logger.LogInformation("Supplies loaded from preconfigured values.");

                return GetPreconfiguredSupplies();
            }

            logger.LogInformation("Supplies loaded from file.");

            return File.ReadAllLines(path)
                .Skip(1) // skip header row
                .Select(x => CreateSupply(x))
                .Where(x => x != null);
        }

        private IEnumerable<Flow> GetPreconfiguredFlows()
        {
            return new List<Flow>()
            {
                new Flow { GeoId = 1, FuelId = 1, SupplyId = 2, Value = 1, Year = 2018 }
            };
        }

        private IEnumerable<Fuel> GetPreconfiguredFuels()
        {
            return new List<Fuel>()
            {
                new Fuel { NameEn = "Petrolium", NameFr = "Pétrole" }
            };
        }

        private IEnumerable<Geo> GetPreconfiguredGeos()
        {
            return new List<Geo>()
            {
                new Geo { NameEn = "Canada", NameFr = "Canada" }
            };
        }

        private IEnumerable<Supply> GetPreconfiguredSupplies()
        {
            return new List<Supply>()
            {
                new Supply { NameEn = "Petrolium", NameFr = "Pétrole" }
            };
        }

        private Flow CreateFlow(string row, SankeyContext context)
        {
            string[] colums = row.Split(";");

            return new Flow
            {
                Year = Convert.ToInt32(colums[0]),
                Geo = context.Geos.Where(p => p.NameEn.Equals(colums[1])).SingleOrDefault(),
                Fuel = context.Fuels.Where(p => p.NameEn.Equals(colums[2])).SingleOrDefault(),
                Supply = context.Supplies.Where(p => p.NameEn.Equals(colums[3])).SingleOrDefault(),
                Value = Convert.ToInt32(colums[4])
            };
        }

        private Fuel CreateFuel(string row)
        {
            string[] colums = row.Split(";");

            return new Fuel
            {
                NameEn = colums[1],
                NameFr = colums[2],
            };
        }

        private Geo CreateGeo(string row)
        {
            string[] colums = row.Split(";");

            return new Geo
            {
                NameEn = colums[1],
                NameFr = colums[2],
                Alpha2 = colums[3],
            };
        }

        private Supply CreateSupply(string row)
        {
            string[] colums = row.Split(";");

            return new Supply
            {
                NameEn = colums[1],
                NameFr = colums[2],
            };
        }
    }
}
