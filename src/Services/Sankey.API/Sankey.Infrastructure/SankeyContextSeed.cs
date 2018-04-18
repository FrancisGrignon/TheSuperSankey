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

            if (false == context.Tables.Any())
            {
                context.Tables.AddRange(GetTablesFromFile(contentRootPath, logger));
                context.SaveChanges();
            }

            if (false == context.Flows.Any())
            {
                var setupPath = Path.Combine(contentRootPath, "Setup");
                var flowPaths = Directory.GetFiles(setupPath).Where(p => p.Contains("Flow"));

                foreach (var path in flowPaths)
                {
                    context.Flows.AddRange(GetFlowsFromPath(path, logger, context));
                    context.SaveChanges();
                }                
            }
        }

        private IEnumerable<Flow> GetFlowsFromPath(string contentPath, ILogger<SankeyContext> logger, SankeyContext context)
        {
            logger.LogInformation($"Flows loaded from {contentPath}.");

            return File.ReadAllLines(contentPath)
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

            logger.LogInformation("Geos loaded from file.");

            return File.ReadAllLines(path)
                .Skip(1) // skip header row
                .Select(x => CreateGeo(x))
                .Where(x => x != null);
        }
        
        private IEnumerable<Node> GetNodesFromFile(string contentRootPath, ILogger<SankeyContext> logger)
        {
            string path = Path.Combine(contentRootPath, "Setup", "Nodes.csv");

            logger.LogInformation("Nodes loaded from file.");

            return File.ReadAllLines(path)
                .Skip(1) // skip header row
                .Select(x => CreateNode(x))
                .Where(x => x != null);
        }
         
        private IEnumerable<Table> GetTablesFromFile(string contentRootPath, ILogger<SankeyContext> logger)
        {
            string path = Path.Combine(contentRootPath, "Setup", "Tables.csv");

            logger.LogInformation("Tables loaded from file.");

            return File.ReadAllLines(path)
                .Skip(1) // skip header row
                .Select(x => CreateTable(x))
                .Where(x => x != null);
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

                var geoName = colums[3].Trim();
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

                var tableTag = colums[5].Trim().ToLower();
                var table = context.Tables.Where(p => p.Tag == tableTag).SingleOrDefault();

                if (null == table)
                {
                    table = new Table
                    {
                        NameEn = tableTag,
                        NameFr = tableTag,
                        NoteEn = "Notes",
                        NoteFr = "Notes",
                        Tag = tableTag
                    };

                    logger.LogInformation($"Missing table {tableTag}");
                }

                return new Flow
                {
                    Source = source,
                    Target = target,
                    Value = Convert.ToInt32(colums[2]),
                    Year = Convert.ToInt32(colums[4]),
                    Geo = geo,
                    Table = table
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
                    Iso3166 = colums[2].ToLower(),
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

         private Table CreateTable(string row)
        {
            string[] colums = row.Split(";");

            try
            {
                return new Table
                {
                    NameEn = colums[0],
                    NameFr = colums[1],
                    Tag = colums[2],
                    NoteEn = colums[3],
                    NoteFr = colums[4],
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} on table {row}.");
            }

            return null;
        }
    }
}
