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

        private int DeleteSectionProperties(IEnumerable<object> ids)
        {
            int success = 1;

            if (ids.Count() != 0)
            {
                string path = m_directory + "\\TextFiles\\" + "SECTION" + ".txt";

                if (File.Exists(path))
                {
                    List<string> stringIndex = ids.Cast<string>().ToList();

                    List<int> indices = stringIndex.Select(x => int.Parse(x)).ToList();

                    List<string> sectionProperties = File.ReadAllLines(path).ToList();
                    List<int> sectionIndexes = new List<int>();

                    foreach (string sectionProperty in sectionProperties)
                    {
                        if (sectionProperty.Contains("*") || sectionProperty.Contains(";") || string.IsNullOrWhiteSpace(sectionProperty))
                        {
                            int clone = 0;
                            sectionIndexes.Add(clone);
                        }
                        else
                        {
                            int clone = int.Parse(sectionProperty.Split(',')[0].Trim());
                            sectionIndexes.Add(clone);
                        }
                    }

                    foreach (int index in indices)
                    {
                        if (sectionIndexes.Contains(index))
                        {
                            int sectionIndex = sectionIndexes.IndexOf(index);
                            sectionProperties[sectionIndex] = "";
                        }
                    }

                    sectionProperties = sectionProperties.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

                    File.Delete(path);
                    File.WriteAllLines(path, sectionProperties.ToArray());
                }
            }
            return success;
        }

        /***************************************************/

    }
}


