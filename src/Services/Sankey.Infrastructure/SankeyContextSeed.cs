using Microsoft.Extensions.Logging;
using Sankey.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sankey.Infrastructure
{
    public class SankeyContextSeed
    {
        public void Seed(SankeyContext context, string contentRootPath, ILogger<SankeyContext> logger)
        {
            if (false == context.Geos.Any())
            {
                context.Geos.AddRange(GetGeosFromFile(contentRootPath, logger));
                context.SaveChanges();
            }

            if (false == context.Nodes.Any())
            {
                context.Nodes.AddRange(GetNodesFromFile(contentRootPath, logger));
                context.SaveChanges();
            }

            if (false == context.Flows.Any())
            {
                context.Flows.AddRange(GetFlowsFromFile(contentRootPath, logger, context));
                context.Flows.AddRange(GetTopLevelFlowsFromFile(contentRootPath, logger, context));                
                context.SaveChanges();
            }
        }

        private IEnumerable<Flow> GetFlowsFromFile(string contentRootPath, ILogger<SankeyContext> logger, SankeyContext context)
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
                .Select(x => CreateFlow(x, logger, context))
                .Where(x => x != null);
        }

         private IEnumerable<Flow> GetTopLevelFlowsFromFile(string contentRootPath, ILogger<SankeyContext> logger, SankeyContext context)
        {
            string path = Path.Combine(contentRootPath, "Setup", "TopLevel.Flows.csv");

            logger.LogInformation("Flows loaded from file.");

            return File.ReadAllLines(path)
                .Skip(1) // skip header row
                .Select(x => CreateFlow(x, logger, context))
                .Where(x => x != null);
        }
     
        private IEnumerable<Geo> GetGeosFromFile(string contentRootPath, ILogger<SankeyContext> logger)
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
        
        private IEnumerable<Node> GetNodesFromFile(string contentRootPath, ILogger<SankeyContext> logger)
        {
            string path = Path.Combine(contentRootPath, "Setup", "Nodes.csv");

            if (false == File.Exists(path))
            {
                logger.LogInformation("Nodes loaded from preconfigured values.");

                return GetPreconfiguredNodes();
            }

            logger.LogInformation("Nodes loaded from file.");

            return File.ReadAllLines(path)
                .Skip(1) // skip header row
                .Select(x => CreateNode(x))
                .Where(x => x != null);
        }

        private IEnumerable<Flow> GetPreconfiguredFlows()
        {
            return new List<Flow>()
            {
                new Flow { GeoId = 1, SourceId = 1, TargetId = 2, Value = 1, Year = 2018 }
            };
        } 

        private IEnumerable<Geo> GetPreconfiguredGeos()
        {
            return new List<Geo>()
            {
                new Geo { NameEn = "Canada", NameFr = "Canada" }
            };
        }

        private IEnumerable<Node> GetPreconfiguredNodes()
        {
            return new List<Node>()
            {
                new Node { NameEn = "Petrolium", NameFr = "Pétrole" }
            };
        }

        private Flow CreateFlow(string row, ILogger<SankeyContext> logger, SankeyContext context)
        {
            string[] colums = row.Split(";");

            try
            {
                var sourceName = colums[0].Trim();
                var source = context.Nodes.Where(p => p.NameEn == sourceName).SingleOrDefault();

                if (null == source)
                {
                    source = new Node
                    {
                        NameEn = sourceName,
                        NameFr = sourceName
                    };

                    logger.LogInformation($"Missing source {sourceName}");
                }

                var targetName = colums[1].Trim();
                var target = context.Nodes.Where(p => p.NameEn == targetName).SingleOrDefault();

                if (null == target)
                {
                    target = new Node
                    {
                        NameEn = targetName,
                        NameFr = targetName
                    };

                    logger.LogInformation($"Missing target {targetName}");
                }

                var geoName = colums[4].Trim();
                var geo = context.Geos.Where(p => p.NameEn == geoName).SingleOrDefault();

                if (null == geo)
                {
                    geo = new Geo
                    {
                        NameEn = geoName,
                        NameFr = geoName
                    };

                    logger.LogInformation($"Missing geo {geoName}");
                }

                return new Flow
                {
                    Source = source,
                    Target = target,
                    Value = Convert.ToInt32(colums[2]),
                    Year = Convert.ToInt32(colums[3]),
                    Geo = geo,
                    Tag = colums[5]
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} on flow {row}.");
            }

            return null;
        }   

        private Geo CreateGeo(string row)
        {
            string[] colums = row.Split(";");

            try
            {
                return new Geo
                {
                    NameEn = colums[0],
                    NameFr = colums[1],
                    Iso3166 = colums[2],
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} on geo {row}.");
            }

            return null;
        }

        private Node CreateNode(string row)
        {
            string[] colums = row.Split(";");

            try
            {
                return new Node
                {
                    NameEn = colums[0],
                    NameFr = colums[1],
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} on node {row}.");
            }

            return null;
        }
    }
}
