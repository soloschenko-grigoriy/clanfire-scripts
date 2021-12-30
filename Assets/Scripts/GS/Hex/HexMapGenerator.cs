using System.Linq;
using GS.Helpers;
using UnityEngine;

namespace GS.Hex
{
    public class HexMapGenerator : MonoBehaviour
    {
        [SerializeField] private HexGrid grid;
        
        [SerializeField][Range(0, 5)] private int SouthPolarEdgeMin = 5;
        [SerializeField][Range(6, 10)] private int SouthPolarEdgeMax = 10;
       
        [SerializeField][Range(11, 15)] private int EquatorSouthEdgeMin = 12;
        [SerializeField][Range(16, 20)] private int EquatorSouthEdgeMax = 16;
        [SerializeField][Range(21, 25)] private int EquatorNorthEdgeMin = 21;
        [SerializeField][Range(26, 30)] private int EquatorNorthEdgeMax = 30;
        
        [SerializeField][Range(40, 45)] private int NorthPolarEdgeMin = 40;
        [SerializeField][Range(45, 50)] private int NorthPolarEdgeMax = 45;
        
        public void Generate()
        {
            foreach (var cell in grid.Cells)
            {
                cell.Cleanup();
            }

            for (int i = 0; i <= 60; i++)
            {
                CreateLandMass(Random.Range(5, 40));
            }
            
            foreach (var cell in grid.Cells)
            {
                if (cell.Category == HexCellCategory.Grass && IsNearPolars(cell))
                {
                    cell.SetCategory(HexCellCategory.Ice);
                    cell.SetElevation(0);
                } 
                else  if (cell.Elevation > 3)
                {
                    grid.ChangeCell(cell, HexCellObjectType.Moutain);
                    cell.SetCategory(HexCellCategory.Mountain);
                    // cell.SetElevation(0);
                } 
                else  if (cell.Elevation > 1)
                {
                    cell.SetCategory(HexCellCategory.Rock);
                    cell.SetElevation(0);

                    if (cell.Neighbors.Any((neighbor) => neighbor?.Category == HexCellCategory.Sand))
                    {
                        cell.SetCategory(HexCellCategory.Dessert);
                    } 
                    else if (cell.Neighbors.Any((neighbor) => neighbor?.Category == HexCellCategory.Mountain) && Random.value > 0.25f)
                    {
                        grid.ChangeCell(cell, HexCellObjectType.Rock);
                    }
                }
                else if (cell.Category == HexCellCategory.DeepWater && IsNearEquator(cell))
                {
                    if (cell.Neighbors.Any((neighbor) => neighbor?.Elevation == 0))
                    {
                        cell.SetCategory(HexCellCategory.ShallowWater);
                    }
                }
                else if (cell.Category == HexCellCategory.Grass)
                {
                    cell.SetElevation(0);
                    
                    if (IsNearEquator(cell) && cell.Neighbors.Any((neighbor) => neighbor?.Category == HexCellCategory.ShallowWater))
                    {
                         cell.SetCategory(HexCellCategory.Sand);
                    } 
                    
                    else if (cell.Neighbors.Any((neighbor) => neighbor?.Category == HexCellCategory.Sand))
                    {
                        cell.SetCategory(HexCellCategory.DryGrass);
                    } 
                     
                    else if(Random.value > 0.35f)
                    {
                        grid.ChangeCell(cell, HexCellObjectType.Forest);
                    }
                } 
            }
            
            
            grid.Refresh();
        }

        private void CreateLandMass(int budget) {
            var center = grid.GetRandomCell();
            var frontier = new PriorityQueue<HexCell>();
            frontier.Enqueue(center, 0);
            
            while (frontier.Count > 0 && budget > 0) {
                var current = frontier.Dequeue();
                if (current.Category == HexCellCategory.Grass)
                {
                    current.SetElevation(current.Elevation + 1);
                    continue;
                }
                
                current.SetCategory(HexCellCategory.Grass);
                current.SetElevation(0);
                budget--;
                
                foreach (var next in current.Neighbors) {
                    if (!next) {
                        continue;
                    }

                    frontier.Enqueue(next, Random.value > 0.5f ? 0 : 1);
                }
            }
            
            // center.SetCategory(HexCellCategory.Building);
        }

        private bool IsNearPolars(HexCell cell)
        {
            return cell.Coordinates.Z < Random.Range(SouthPolarEdgeMin, SouthPolarEdgeMax) ||
                   cell.Coordinates.Z > Random.Range(NorthPolarEdgeMin, NorthPolarEdgeMax);
        }

        private bool IsNearEquator(HexCell cell)
        {
            return cell.Coordinates.Z > Random.Range(EquatorSouthEdgeMin, EquatorSouthEdgeMax) &&
                   cell.Coordinates.Z < Random.Range(EquatorNorthEdgeMin, EquatorNorthEdgeMax);
        }
    }
}
