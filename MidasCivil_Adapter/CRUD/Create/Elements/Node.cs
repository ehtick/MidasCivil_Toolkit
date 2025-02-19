/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2022, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *                                           
 *                                                                              
 * The BHoM is free software: you can redistribute it and/or modify         
 * it under the terms of the GNU Lesser General Public License as published by  
 * the Free Software Foundation, either version 3.0 of the License, or          
 * (at your option) any later version.                                          
 *                                                                              
 * The BHoM is distributed in the hope that it will be useful,              
 * but WITHOUT ANY WARRANTY; without even the implied warranty of               
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 
 * GNU Lesser General Public License for more details.                          
 *                                                                            
 * You should have received a copy of the GNU Lesser General Public License     
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.      
 */

using BH.Engine.Adapter;
using BH.Engine.Structure;
using BH.oM.Adapters.MidasCivil;
using BH.oM.Structure.Elements;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace BH.Adapter.MidasCivil
{
    public partial class MidasCivilAdapter
    {
        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/

        private bool CreateCollection(IEnumerable<Node> nodes)
        {
            string nodePath = CreateSectionFile("NODE");
            List<string> midasNodes = new List<string>();

            CreateGroups(nodes);

            foreach (Node node in nodes)
            {
                if (!(node.Support == null))
                {
                    if (MidasCivilAdapter.GetStiffnessVectorModulus(node.Support) > 0)
                    {
                        AssignProperty(node.AdapterId<string>(typeof(MidasCivilId)), new string(node.Support.DescriptionOrName().Replace(",", "").Take(m_groupCharacterLimit).ToArray()), "SPRING");
                    }
                    else
                    {
                        AssignProperty(node.AdapterId<string>(typeof(MidasCivilId)), new string(node.Support.DescriptionOrName().Replace(",", "").Take(m_groupCharacterLimit).ToArray()), "CONSTRAINT");
                    }

                }
                midasNodes.Add(Adapters.MidasCivil.Convert.FromNode(node, m_lengthUnit));
            }

            File.AppendAllLines(nodePath, midasNodes);

            return true;
        }

        /***************************************************/

    }
}

