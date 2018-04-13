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
                context.SaveChangesAsync();
            }

            if (false == context.Nodes.Any())
            {
                context.Nodes.AddRange(GetNodesFromFile(contentRootPath, logger));
                context.SaveChangesAsync();
            }

            if (false == context.Flows.Any())
            {
                context.Flows.AddRange(GetFlowsFromFile(contentRootPath, logger, context));
                context.SaveChangesAsync();
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
                .Select(x => CreateFlow(x, context))
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

        private Flow CreateFlow(string row, SankeyContext context)
        {
            string[] colums = row.Split(";");

            return new Flow
            {
                Year = Convert.ToInt32(colums[0]),
                Geo = context.Geos.Where(p => p.NameEn.Equals(colums[1])).SingleOrDefault(),
                Source = context.Nodes.Where(p => p.NameEn.Equals(colums[2])).SingleOrDefault(),
                Target = context.Nodes.Where(p => p.NameEn.Equals(colums[3])).SingleOrDefault(),
                Value = Convert.ToInt32(colums[4])
            };
        }   

        private Geo CreateGeo(string row)
        {
            string[] colums = row.Split(";");

            return new Geo
            {
                NameEn = colums[0],
                NameFr = colums[1],
                Iso3166 = colums[2],
            };
        }

        private Node CreateNode(string row)
        {
            string[] colums = row.Split(";");

            return new Node
            {
                NameEn = colums[0],
                NameFr = colums[1],
            };
        }
    }
}
