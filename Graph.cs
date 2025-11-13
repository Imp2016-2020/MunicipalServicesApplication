using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp
{
    public class Graph
    {
        private Dictionary<string, List<GraphEdge>> adjacencyList;
        private List<ServiceRequest> requests;

        public Graph()
        {
            adjacencyList = new Dictionary<string, List<GraphEdge>>();
            requests = new List<ServiceRequest>();
        }

        public void AddRequest(ServiceRequest request)
        {
            if (!adjacencyList.ContainsKey(request.RequestId))
            {
                adjacencyList[request.RequestId] = new List<GraphEdge>();
                requests.Add(request);
            }

            // Create edges based on location similarity
            foreach (var existingRequest in requests)
            {
                if (existingRequest.RequestId != request.RequestId &&
                    AreRequestsRelated(request, existingRequest))
                {
                    double weight = CalculateRelationshipWeight(request, existingRequest);
                    AddEdge(request.RequestId, existingRequest.RequestId, weight);
                }
            }
        }

        private bool AreRequestsRelated(ServiceRequest req1, ServiceRequest req2)
        {
            // Requests are related if they share same category or nearby locations
            return req1.Category == req2.Category ||
                   CalculateLocationProximity(req1.Location, req2.Location) < 5.0;
        }

        private double CalculateLocationProximity(string loc1, string loc2)
        {
            // Simplified proximity calculation
            // In real implementation, this would use geocoding
            return Math.Abs(loc1.GetHashCode() - loc2.GetHashCode()) % 10;
        }

        private double CalculateRelationshipWeight(ServiceRequest req1, ServiceRequest req2)
        {
            double weight = 0;
            if (req1.Category == req2.Category) weight += 2;
            if (CalculateLocationProximity(req1.Location, req2.Location) < 3) weight += 3;
            if (req1.Priority == req2.Priority) weight += 1;
            return weight;
        }

        private void AddEdge(string from, string to, double weight)
        {
            if (!adjacencyList.ContainsKey(from))
                adjacencyList[from] = new List<GraphEdge>();

            if (!adjacencyList.ContainsKey(to))
                adjacencyList[to] = new List<GraphEdge>();

            adjacencyList[from].Add(new GraphEdge(to, weight));
            adjacencyList[to].Add(new GraphEdge(from, weight));
        }

        public List<string> BreadthFirstSearch(string startRequestId)
        {
            var visited = new HashSet<string>();
            var queue = new Queue<string>();
            var result = new List<string>();

            if (!adjacencyList.ContainsKey(startRequestId))
                return result;

            visited.Add(startRequestId);
            queue.Enqueue(startRequestId);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (var edge in adjacencyList[current])
                {
                    if (!visited.Contains(edge.Target))
                    {
                        visited.Add(edge.Target);
                        queue.Enqueue(edge.Target);
                    }
                }
            }

            return result;
        }

        public List<string> DepthFirstSearch(string startRequestId)
        {
            var visited = new HashSet<string>();
            var result = new List<string>();
            DFSRecursive(startRequestId, visited, result);
            return result;
        }

        private void DFSRecursive(string current, HashSet<string> visited, List<string> result)
        {
            visited.Add(current);
            result.Add(current);

            if (adjacencyList.ContainsKey(current))
            {
                foreach (var edge in adjacencyList[current])
                {
                    if (!visited.Contains(edge.Target))
                    {
                        DFSRecursive(edge.Target, visited, result);
                    }
                }
            }
        }

        public string GetAnalysisReport()
        {
            var report = new StringBuilder();
            report.AppendLine($"Total Nodes: {adjacencyList.Count}");
            report.AppendLine($"Total Edges: {adjacencyList.Sum(x => x.Value.Count) / 2}");

            if (adjacencyList.Count > 0)
            {
                var firstNode = adjacencyList.Keys.First();
                var bfsResult = BreadthFirstSearch(firstNode);
                report.AppendLine($"BFS from {firstNode}: {string.Join(" -> ", bfsResult)}");

                var dfsResult = DepthFirstSearch(firstNode);
                report.AppendLine($"DFS from {firstNode}: {string.Join(" -> ", dfsResult)}");
            }

            return report.ToString();
        }

        public string GetVisualization()
        {
            var sb = new StringBuilder();
            sb.AppendLine("REQUEST RELATIONSHIP GRAPH:");
            sb.AppendLine("============================");

            foreach (var node in adjacencyList)
            {
                sb.AppendLine($"{node.Key} connected to:");
                foreach (var edge in node.Value)
                {
                    sb.AppendLine($"  -> {edge.Target} (weight: {edge.Weight:F2})");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }

    public class GraphEdge
    {
        public string Target { get; set; }
        public double Weight { get; set; }

        public GraphEdge(string target, double weight)
        {
            Target = target;
            Weight = weight;
        }
    }
}