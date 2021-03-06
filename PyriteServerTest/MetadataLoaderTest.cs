﻿// // //------------------------------------------------------------------------------------------------- 
// // // <copyright file="MetadataLoaderTest.cs" company="Microsoft Corporation">
// // // Copyright (c) Microsoft Corporation. All rights reserved.
// // // </copyright>
// // //-------------------------------------------------------------------------------------------------

namespace PyriteServerTest
{
    using System.IO;
    using PyriteServer.DataAccess;
    using PyriteServer.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;

    [TestClass]
    [DeploymentItem(@"data\", "data")]
    public class MetadataLoaderTest
    {
        [TestMethod]
        public void LoadDefaultMinSize()
        {
            OcTree<CubeBounds> ocTree;
            using (Stream metadataStream = new FileStream(".\\data\\validdataset1\\v1\\metadata.json", FileMode.Open, FileAccess.Read))
            {
                ocTree = MetadataLoader.Load(metadataStream, "L1", new Vector3(1,1,1));
            }

            ocTree.UpdateTree();
            Assert.IsNotNull(ocTree);
            Assert.IsTrue(ocTree.HasChildren);

            OcTreeUtilities.Dump(ocTree);
        }

        [TestMethod]
        public void LoadDefaultSize2()
        {
            OcTree<CubeBounds> ocTree = new OcTree<CubeBounds>(new BoundingBox(Vector3.Zero, Vector3.Zero), new CubeBounds[]{}, 2);
            using (Stream metadataStream = new FileStream(".\\data\\validdataset1\\v1\\metadata.json", FileMode.Open, FileAccess.Read))
            {
                ocTree = MetadataLoader.Load(metadataStream, ocTree, "L1", new Vector3(1, 1, 1));
            }

            ocTree.UpdateTree();
            Assert.AreEqual(2, ocTree.MinimumSize);
            Assert.IsNotNull(ocTree);
            Assert.IsTrue(ocTree.HasChildren);

            OcTreeUtilities.Dump(ocTree);   
        }

        [TestMethod]
        public void LoadLargeSet()
        {
            OcTree<CubeBounds> ocTree = new OcTree<CubeBounds>(new BoundingBox(Vector3.Zero, Vector3.Zero), new CubeBounds[] { }, 2);
            using (Stream metadataStream = new FileStream(".\\data\\validdataset2\\v1\\metadata.json", FileMode.Open, FileAccess.Read))
            {
                ocTree = MetadataLoader.Load(metadataStream, ocTree, "L1", new Vector3(1,1,1));
            }

            ocTree.UpdateTree();
            Assert.AreEqual(2, ocTree.MinimumSize);
            Assert.IsNotNull(ocTree);
            Assert.IsTrue(ocTree.HasChildren);

            OcTreeUtilities.Dump(ocTree);
        }
    }
}