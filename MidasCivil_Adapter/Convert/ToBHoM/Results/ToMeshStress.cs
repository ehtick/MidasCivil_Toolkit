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

using BH.oM.Structure.Loads;
using BH.oM.Structure.Results;
using System.Linq;
using System.Collections.Generic;
using BH.oM.Geometry;

namespace BH.Adapter.MidasCivil
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static MeshStress ToMeshStress(this List<string> delimitted)
        {
            double LayerPosition = 1;
            MeshResultLayer meshResultLayer = MeshResultLayer.Upper;

            if (delimitted[8].Contains("Bot"))
            {
                LayerPosition = 0;
                meshResultLayer = MeshResultLayer.Lower;
            }
            //TODO: resolve below identifiers extractable through the API
            int mode = -1;
            double timeStep = 0;

            MeshStress Meshstress = new MeshStress(System.Convert.ToInt32(delimitted[2]), delimitted[7], 0,
            delimitted[3], mode, timeStep, meshResultLayer, LayerPosition, MeshResultSmoothingType.None, null,
            System.Convert.ToDouble(delimitted[9]), System.Convert.ToDouble(delimitted[10]), 0,
            System.Convert.ToDouble(delimitted[11]), System.Convert.ToDouble(delimitted[11]), System.Convert.ToDouble(delimitted[12]),
            System.Convert.ToDouble(delimitted[13]), 0);

            return Meshstress;
        }

        /***************************************************/

    }
}

