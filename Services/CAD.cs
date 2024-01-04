using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FireAlarmTool.Models;

// import autocad dependancies
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace FireAlarmTool.Services
{
    public class CAD
    {
        public List<FireAlarmDevice> ScanDrawingAndInsert(List<FireAlarmDevice> fd, List<BlockMap> blkMap)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            List<FireAlarmDevice> fds = fd;

            // find all "RoomLabel" dynamic blocks
            ObjectIdCollection objIDs = GetDynamicBlocksByName("RoomLabel");

            List<RoomLabelScan> list = new List<RoomLabelScan>();

            using (var t = db.TransactionManager.StartTransaction())
            {

                foreach(ObjectId id in objIDs)
                {
                    DBObject dbObject = t.GetObject(id, OpenMode.ForRead) as DBObject;
                    if (dbObject == null)
                        continue;

                    if(dbObject is BlockReference)
                    {
                        BlockReference blockReference = t.GetObject(id, OpenMode.ForRead) as BlockReference;
                        if (blockReference == null)
                            continue;
                        if (blockReference.AttributeCollection == null)
                            continue;

                        AttributeCollection attrCollection = blockReference.AttributeCollection;

                        RoomLabelScan rls = new RoomLabelScan
                        {
                            BlockId = id,
                            InsertPoint = blockReference.Position
                        };
                        RoomLabel _roomLabel = new RoomLabel();

                        foreach (ObjectId objectId in attrCollection)
                        {
                            AttributeReference attrRef = t.GetObject(objectId, OpenMode.ForRead) as AttributeReference;
                            switch(attrRef.Tag.ToUpper())
                            {
                                case "RNAME":
                                    _roomLabel.RNAME = attrRef.TextString;
                                    break;
                                case "RNAME2":
                                    _roomLabel.RNAME2 = attrRef.TextString;
                                    break;
                                case "ROOMNUMBER":
                                    _roomLabel.RoomNumber = attrRef.TextString;
                                    break;
                                case "BLOCK":
                                    _roomLabel.BLOCKREF = attrRef.TextString;
                                    break;
                                case "BLOCKREF":
                                    // unused
                                    break;
                                case "LUX":
                                    // unused
                                    break;
                            }
                        }

                        rls.RoomLabel = _roomLabel;
                        list.Add(rls);

                    }

                }

                // commit changes - close transaction
                t.Commit();
            }

            foreach(FireAlarmDevice fa in fds)
            {
                // if already deselected, skip
                if (!fa.Selected)
                    continue;

                foreach (RoomLabelScan roomLabelScan in list)
                {
                    // check room labels against fire alarm device data
                    if(roomLabelScan.RoomLabel.BLOCKREF == fa.BlockReference && roomLabelScan.RoomLabel.RoomNumber == fa.RoomNumber)
                    {
                        // got the insertion point
                        // figure out what block is needed
                        foreach (var bm in blkMap)
                        {
                            if (bm.DeviceType == fa.DeviceType)
                            {
                                InsertBlock(bm.BlockName, roomLabelScan.InsertPoint, fa);
                                fa.Selected = false;
                                continue;
                            }
                        }

                        
                    }
                }
            }

            // scans drawings for existing 
            // returns fd with found devices unselected
            return fds;
        }
        public List<P4Tag> ScanDrawingAndInsert(List<P4Tag> p4tags)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            List<P4Tag> updatedP4Tags = p4tags;

            // find all "RoomLabel" dynamic blocks
            ObjectIdCollection objIDs = GetDynamicBlocksByName("RoomLabel");

            List<RoomLabelScan> list = new List<RoomLabelScan>();

            using (var t = db.TransactionManager.StartTransaction())
            {

                foreach (ObjectId id in objIDs)
                {
                    DBObject dbObject = t.GetObject(id, OpenMode.ForRead) as DBObject;
                    if (dbObject == null)
                        continue;

                    if (dbObject is BlockReference)
                    {
                        BlockReference blockReference = t.GetObject(id, OpenMode.ForRead) as BlockReference;
                        if (blockReference == null)
                            continue;
                        if (blockReference.AttributeCollection == null)
                            continue;

                        AttributeCollection attrCollection = blockReference.AttributeCollection;

                        RoomLabelScan rls = new RoomLabelScan
                        {
                            BlockId = id,
                            InsertPoint = blockReference.Position
                        };
                        RoomLabel _roomLabel = new RoomLabel();

                        foreach (ObjectId objectId in attrCollection)
                        {
                            AttributeReference attrRef = t.GetObject(objectId, OpenMode.ForRead) as AttributeReference;
                            switch (attrRef.Tag.ToUpper())
                            {
                                case "RNAME":
                                    _roomLabel.RNAME = attrRef.TextString;
                                    break;
                                case "RNAME2":
                                    _roomLabel.RNAME2 = attrRef.TextString;
                                    break;
                                case "ROOMNUMBER":
                                    _roomLabel.RoomNumber = attrRef.TextString;
                                    break;
                                case "BLOCK":
                                    _roomLabel.BLOCKREF = attrRef.TextString;
                                    break;
                                case "BLOCKREF":
                                    // unused
                                    break;
                                case "LUX":
                                    // unused
                                    break;
                            }
                        }

                        rls.RoomLabel = _roomLabel;
                        list.Add(rls);

                    }

                }

                // commit changes - close transaction
                t.Commit();
            }

            foreach (P4Tag tag in updatedP4Tags)
            {
                // if already deselected, skip
                if (!tag.Selected)
                    continue;

                foreach (RoomLabelScan roomLabelScan in list)
                {
                    // check room labels against fire alarm device data
                    if(tag.BlockRef.Length > 1)
                    {
                        // split the tag at '.'
                        var splitBlockRef = tag.BlockRef.Split('.');
                        if (splitBlockRef.Length > 1)
                        {
                            // check if the block reference and room numbers match
                            if(roomLabelScan.RoomLabel.BLOCKREF == splitBlockRef[0] && roomLabelScan.RoomLabel.RoomNumber == splitBlockRef[1])
                            {
                                InsertBlock("P4 Reference Block", roomLabelScan.InsertPoint, tag);
                                tag.Selected = false;
                                continue;
                            }
                        }
                    }
                }
            }

            // scans drawings for existing 
            // returns fd with found devices unselected
            return updatedP4Tags;
        }

        public List<RoomLabel> ScanDrawingAndInsert(List<RoomLabel> rm)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            List<RoomLabel> rms = rm;

            // find all MText Objects
            ObjectIdCollection objIDs = GetAllMTextObjectIds();

            List<MTextScan> list = new List<MTextScan>();

            using (var t = db.TransactionManager.StartTransaction())
            {

                foreach (ObjectId id in objIDs)
                {
                    DBObject dbObject = t.GetObject(id, OpenMode.ForRead) as DBObject;
                    if (dbObject == null)
                        continue;

                    if (dbObject is MText)
                    {
                        MText mText = t.GetObject(id, OpenMode.ForRead) as MText;
                        if (mText == null)
                            continue;
                        if (mText.Text == "")
                            continue;

                        MTextScan textScan = new MTextScan
                        {
                            InsertionPoint = mText.Location,
                            MTextString = mText.Text
                        };

                        list.Add(textScan);
                    }

                }

                // commit changes - close transaction
                t.Commit();
            }

            foreach (RoomLabel label in rms)
            {
                // if already deselected, skip
                if (!label.Selected)
                    continue;

                foreach(MTextScan text in list)
                {
                    string search = String.Concat(label.BLOCKREF + "-" + label.RoomNumber); // 00-200 example.
                    if(search == text.MTextString)
                    {
                        // insert block on text insertion point
                        InsertBlock("RoomLabel", text.InsertionPoint, label);
                        label.Selected = false;
                        continue;
                    }
                }

            }

            // scans drawings for existing 
            // returns fd with found devices unselected
            return rms;
        }

        public List<FireAlarmDevice> DeselectInserted(List<FireAlarmDevice> fd, List<BlockMap> blkMap)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            List<FireAlarmDevice> fds = fd;

            // find all Fire Alarm mapped dynamic blocks
            List<FireAlarmDevice> existingDevices = new List<FireAlarmDevice>();

            foreach (BlockMap bm in blkMap)
            {
                if (bm.BlockName == null)
                    continue;

                ObjectIdCollection objIDs = GetDynamicBlocksByName(bm.BlockName);

                using (var t = db.TransactionManager.StartTransaction())
                {

                    foreach (ObjectId id in objIDs)
                    {
                        DBObject dbObject = t.GetObject(id, OpenMode.ForRead) as DBObject;
                        if (dbObject == null)
                            continue;

                        if (dbObject is BlockReference)
                        {
                            BlockReference blockReference = t.GetObject(id, OpenMode.ForRead) as BlockReference;
                            if (blockReference == null)
                                continue;
                            if (blockReference.AttributeCollection == null)
                                continue;

                            AttributeCollection attrCollection = blockReference.AttributeCollection;

                            FireAlarmDevice fad = new FireAlarmDevice();

                            foreach (ObjectId objectId in attrCollection)
                            {
                                AttributeReference attrRef = t.GetObject(objectId, OpenMode.ForRead) as AttributeReference;
                                switch (attrRef.Tag.ToUpper())
                                {
                                    case "LP":
                                        fad.Loop = attrRef.TextString;
                                        break;
                                    case "ADD":
                                        fad.Address = attrRef.TextString;
                                        break;
                                    case "ZONE":
                                        fad.PriZone = attrRef.TextString;
                                        break;
                                    case "ROOMNAME":
                                        fad.LocationText = attrRef.TextString;
                                        break;
                                    case "BLOCKREF":
                                        //var split = attrRef.TextString.Split('.').ToArray();
                                        //if(split.Length > 1)
                                        //{
                                        //    fad.BlockReference = split[0];
                                        //    fad.RoomNumber = split[1];
                                        //}
                                        //else
                                        //{
                                        //    fad.BlockReference = "";
                                        //    fad.RoomNumber = "";
                                        //}
                                        
                                        break;
                                    default:
                                        break;
                                }
                            }

                            existingDevices.Add(fad);

                        }

                    }

                    // commit changes - close transaction
                    t.Commit();
                }

            }
         
            foreach (FireAlarmDevice fa in fds)
            {
                foreach (FireAlarmDevice fad in existingDevices)
                {
                    // check against fire alarm device data
                    if(fa.Loop == fad.Loop && fa.Address == fad.Address && fa.PriZone == fad.PriZone)
                    {
                        fa.Selected = false;
                    }
                }
            }

            // scans drawings for existing 
            // returns fd with found devices unselected
            return fds;
        }

        public List<RoomLabel> DeselectInserted(List<RoomLabel> rm)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            List<RoomLabel> rms = rm;

            // find all Fire Alarm mapped dynamic blocks
            List<RoomLabel> existingLabels = new List<RoomLabel>();

            ObjectIdCollection objIDs = GetDynamicBlocksByName("RoomLabel");

            using (var t = db.TransactionManager.StartTransaction())
            {

                foreach (ObjectId id in objIDs)
                {
                    DBObject dbObject = t.GetObject(id, OpenMode.ForRead) as DBObject;
                    if (dbObject == null)
                        continue;

                    if (dbObject is BlockReference)
                    {
                        BlockReference blockReference = t.GetObject(id, OpenMode.ForRead) as BlockReference;
                        if (blockReference == null)
                            continue;
                        if (blockReference.AttributeCollection == null)
                            continue;

                        AttributeCollection attrCollection = blockReference.AttributeCollection;

                        RoomLabel roomLabel = new RoomLabel();

                        foreach (ObjectId objectId in attrCollection)
                        {
                            AttributeReference attrRef = t.GetObject(objectId, OpenMode.ForRead) as AttributeReference;
                            switch (attrRef.Tag.ToUpper())
                            {
                                case "RNAME":
                                    roomLabel.RNAME = attrRef.TextString;
                                    break;
                                case "RNAME2":
                                    roomLabel.RNAME2 = attrRef.TextString;
                                    break;
                                case "BLOCK":
                                    roomLabel.BLOCKREF = attrRef.TextString;
                                    break;
                                case "ROOMNUMBER":
                                    roomLabel.RoomNumber = attrRef.TextString;
                                    break;
                                default:
                                    break;
                            }
                        }

                        existingLabels.Add(roomLabel);

                    }

                }

                // commit changes - close transaction
                t.Commit();
            }

            foreach (RoomLabel _roomLabel in rms)
            {
                foreach (RoomLabel existingLabel in existingLabels)
                {
                    // check against room label data
                    if(_roomLabel.BLOCKREF == existingLabel.BLOCKREF && existingLabel.RoomNumber == existingLabel.RoomNumber)
                    {
                        _roomLabel.Selected = false;
                    }
                }
            }

            // scans drawings for existing 
            // returns fd with found devices unselected
            return rms;
        }

        public List<P4Tag> DeselectInserted(List<P4Tag> p4tags)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            List<P4Tag> updatedp4tags = p4tags;

            // find all Fire Alarm mapped dynamic blocks
            List<P4Tag> existingP4Tags = new List<P4Tag>();

            ObjectIdCollection objIDs = GetDynamicBlocksByName("P4 Reference Block");

            using (var t = db.TransactionManager.StartTransaction())
            {

                foreach (ObjectId id in objIDs)
                {
                    DBObject dbObject = t.GetObject(id, OpenMode.ForRead) as DBObject;
                    if (dbObject == null)
                        continue;

                    if (dbObject is BlockReference)
                    {
                        BlockReference blockReference = t.GetObject(id, OpenMode.ForRead) as BlockReference;
                        if (blockReference == null)
                            continue;
                        if (blockReference.AttributeCollection == null)
                            continue;

                        AttributeCollection attrCollection = blockReference.AttributeCollection;

                        P4Tag _p4tag = new P4Tag();

                        foreach (ObjectId objectId in attrCollection)
                        {
                            AttributeReference attrRef = t.GetObject(objectId, OpenMode.ForRead) as AttributeReference;
                            switch (attrRef.Tag.ToUpper())
                            {
                                case "CB#":
                                    _p4tag.CollectorBoxNumber = attrRef.TextString;
                                    break;
                                case "NUM":
                                    _p4tag.Address = attrRef.TextString;
                                    break;
                                case "TYPE":
                                    _p4tag.Type = attrRef.TextString;
                                    break;
                                case "BLOCKREF":
                                    _p4tag.BlockRef = attrRef.TextString;
                                    break;
                                case "ROOMNAME":
                                    _p4tag.RoomName = attrRef.TextString;
                                    break;
                                default:
                                    break;
                            }
                        }

                        existingP4Tags.Add(_p4tag);

                    }

                }

                // commit changes - close transaction
                t.Commit();
            }

            foreach (P4Tag _p4Tag in updatedp4tags)
            {
                foreach (P4Tag existingp4Tag in existingP4Tags)
                {
                    // check against room label data
                    if(_p4Tag.CollectorBoxNumber == existingp4Tag.CollectorBoxNumber && _p4Tag.Address == existingp4Tag.Address && _p4Tag.Type == existingp4Tag.Type)
                    {
                        _p4Tag.Selected = false;
                    }
                }
            }

            // scans drawings for existing 
            // returns fd with found devices unselected
            return updatedp4tags;
        }

        public ObjectIdCollection GetDynamicBlocksByName(string BlkName)
        {
            ObjectIdCollection res = new ObjectIdCollection();
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                //get the blockTable and iterate through all block Defs
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                foreach (ObjectId btrId in bt)
                {
                    //get the block Def and see if it is anonymous
                    BlockTableRecord btr = (BlockTableRecord)trans.GetObject(btrId, OpenMode.ForRead);
                    if (btr.IsDynamicBlock && btr.Name.Equals(BlkName))
                    {
                        // get block ids as well as anonymous block ids - all encompassing
                        //get all anonymous blocks from this dynamic block
                        ObjectIdCollection anonymousIds = btr.GetAnonymousBlockIds();
                        ObjectIdCollection dynBlockRefs = new ObjectIdCollection();
                        ObjectIdCollection blockIds = btr.GetBlockReferenceIds(true, true);
                        foreach (ObjectId BtrId in blockIds)
                        {
                            dynBlockRefs.Add(BtrId);
                        }
                        foreach (ObjectId anonymousBtrId in anonymousIds)
                        {
                            //get the anonymous block
                            BlockTableRecord anonymousBtr = (BlockTableRecord)trans.GetObject(anonymousBtrId, OpenMode.ForRead);
                            //and all references to this block
                            ObjectIdCollection blockRefIds = anonymousBtr.GetBlockReferenceIds(true, true);
                            foreach (ObjectId id in blockRefIds)
                            {
                                dynBlockRefs.Add(id);
                            }
                        }
                        res = dynBlockRefs;
                        break;
                    }
                }
                trans.Commit();
            }
            return res;
        }

        public ObjectIdCollection GetAllMTextObjectIds()
        {
            ObjectIdCollection res = new ObjectIdCollection();
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                //get the blockTable and iterate through all block Defs
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                foreach (ObjectId btrId in bt)
                {
                    BlockTableRecord btr = (BlockTableRecord)trans.GetObject(btrId, OpenMode.ForRead);

                    foreach(ObjectId id in btr)
                    {
                        DBObject dbObject = (DBObject)trans.GetObject(id, OpenMode.ForRead);
                        if (dbObject == null)
                            continue;

                        if(dbObject is MText)
                        {
                            res.Add(id);
                        }
                    }

                }
                trans.Commit();
            }
            return res;
        }
        public Point3d GetInsertPoint(string contextString = "")
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;

            PromptPointResult pointResult;
            PromptPointOptions promptPointOptions = new PromptPointOptions("")
            {
                Message = "\nDefine insertion point for new block: " + contextString
            };

            pointResult = doc.Editor.GetPoint(promptPointOptions);

            if(pointResult.Status == PromptStatus.OK)
            {
                Point3d insertPoint = pointResult.Value;
                return insertPoint;
            }
            else
            {
                throw new System.Exception("User cancelled operation.");
            }
        }

        public string[] FindAllBlocks()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            List<string> outputStringList = new List<string>();

            using(var t = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = t.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                foreach(ObjectId btrId in bt)
                {
                    BlockTableRecord btr = t.GetObject(btrId, OpenMode.ForRead) as BlockTableRecord;

                    if(btr.IsDynamicBlock)
                    {
                        outputStringList.Add(btr.Name);
                    }
                }

                t.Commit();
            }

            return outputStringList.ToArray();
        }

        // Fire InsertBlock function
        public void InsertBlock(string blockName, Point3d insertPoint, FireAlarmDevice fd)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (var docLock = doc.LockDocument())
            {
                using (var t = db.TransactionManager.StartTransaction())
                {
                    // Open block table record to read all blocks
                    BlockTable blockTable = t.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    ObjectId blockRecordId = ObjectId.Null;

                    if (blockTable.Has(blockName))
                    {
                        blockRecordId = blockTable[blockName];
                    }

                    if (blockRecordId != ObjectId.Null)
                    {
                        BlockTableRecord blockTableRecord = t.GetObject(blockRecordId, OpenMode.ForRead) as BlockTableRecord; // read in the template block
                                                                                                                              // create new block reference with selected insertion point
                        using (var blockReference = new BlockReference(insertPoint, blockRecordId))
                        {
                            BlockTableRecord currentSpaceBlockTableRecord = t.GetObject(db.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord; // open block table record for writing new block
                            currentSpaceBlockTableRecord.AppendEntity(blockReference);
                            t.AddNewlyCreatedDBObject(blockReference, true);

                            // check that the block has the attribute definitions
                            if (blockTableRecord.HasAttributeDefinitions)
                            {
                                foreach (ObjectId objectId in blockTableRecord)
                                {
                                    DBObject databaseObject = t.GetObject(objectId, OpenMode.ForWrite) as DBObject;
                                    if (databaseObject is AttributeDefinition)
                                    {
                                        AttributeDefinition attributeDefinition = databaseObject as AttributeDefinition;

                                        if (!attributeDefinition.Constant)
                                        {
                                            using (AttributeReference attributeReference = new AttributeReference())
                                            {
                                                attributeReference.SetAttributeFromBlock(attributeDefinition, blockReference.BlockTransform);
                                                attributeReference.Position = attributeDefinition.Position.TransformBy(blockReference.BlockTransform);

                                                //attributeReference.TextString = acAtt.TextString;
                                                attributeReference.TextString = UpdateAttribute(attributeReference, attributeDefinition.TextString, fd);

                                                blockReference.AttributeCollection.AppendAttribute(attributeReference);

                                                t.AddNewlyCreatedDBObject(attributeReference, true);
                                            }
                                        }
                                    }

                                    // Visibility1 = "All text"
                                    DynamicBlockReferencePropertyCollection pc = blockReference.DynamicBlockReferencePropertyCollection;

                                    foreach(DynamicBlockReferenceProperty prop in pc)
                                    {
                                        if (prop.PropertyName.ToUpper() == "VISIBILITY1")
                                        {
                                            prop.Value = "All text";
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // commit changes!
                    t.Commit();
                }
            }
        }

        // Room Label function
        public void InsertBlock(string blockName, Point3d insertPoint, RoomLabel label)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (var docLock = doc.LockDocument())
            {
                using (var t = db.TransactionManager.StartTransaction())
                {
                    // Open block table record to read all blocks
                    BlockTable blockTable = t.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    ObjectId blockRecordId = ObjectId.Null;

                    if (blockTable.Has(blockName))
                    {
                        blockRecordId = blockTable[blockName];
                    }

                    if (blockRecordId != ObjectId.Null)
                    {
                        BlockTableRecord blockTableRecord = t.GetObject(blockRecordId, OpenMode.ForRead) as BlockTableRecord; // read in the template block
                                                                                                                              // create new block reference with selected insertion point
                        using (var blockReference = new BlockReference(insertPoint, blockRecordId))
                        {
                            BlockTableRecord currentSpaceBlockTableRecord = t.GetObject(db.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord; // open block table record for writing new block
                            currentSpaceBlockTableRecord.AppendEntity(blockReference);
                            t.AddNewlyCreatedDBObject(blockReference, true);

                            // check that the block has the attribute definitions
                            if (blockTableRecord.HasAttributeDefinitions)
                            {
                                foreach (ObjectId objectId in blockTableRecord)
                                {
                                    DBObject databaseObject = t.GetObject(objectId, OpenMode.ForWrite) as DBObject;
                                    if (databaseObject is AttributeDefinition)
                                    {
                                        AttributeDefinition attributeDefinition = databaseObject as AttributeDefinition;

                                        if (!attributeDefinition.Constant)
                                        {
                                            using (AttributeReference attributeReference = new AttributeReference())
                                            {
                                                attributeReference.SetAttributeFromBlock(attributeDefinition, blockReference.BlockTransform);
                                                attributeReference.Position = attributeDefinition.Position.TransformBy(blockReference.BlockTransform);

                                                //attributeReference.TextString = acAtt.TextString;
                                                attributeReference.TextString = UpdateAttribute(attributeReference, attributeDefinition.TextString, label);

                                                blockReference.AttributeCollection.AppendAttribute(attributeReference);

                                                t.AddNewlyCreatedDBObject(attributeReference, true);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // commit changes!
                    t.Commit();
                }
            }
        }

        public void InsertBlock(string blockName, Point3d insertPoint, P4Tag tag)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (var docLock = doc.LockDocument())
            {
                using (var t = db.TransactionManager.StartTransaction())
                {
                    // Open block table record to read all blocks
                    BlockTable blockTable = t.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    ObjectId blockRecordId = ObjectId.Null;

                    if (blockTable.Has(blockName))
                    {
                        blockRecordId = blockTable[blockName];
                    }

                    if (blockRecordId != ObjectId.Null)
                    {
                        BlockTableRecord blockTableRecord = t.GetObject(blockRecordId, OpenMode.ForRead) as BlockTableRecord; // read in the template block
                                                                                                                              // create new block reference with selected insertion point
                        using (var blockReference = new BlockReference(insertPoint, blockRecordId))
                        {
                            BlockTableRecord currentSpaceBlockTableRecord = t.GetObject(db.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord; // open block table record for writing new block
                            currentSpaceBlockTableRecord.AppendEntity(blockReference);
                            t.AddNewlyCreatedDBObject(blockReference, true);

                            // check that the block has the attribute definitions
                            if (blockTableRecord.HasAttributeDefinitions)
                            {
                                foreach (ObjectId objectId in blockTableRecord)
                                {
                                    DBObject databaseObject = t.GetObject(objectId, OpenMode.ForWrite) as DBObject;
                                    if (databaseObject is AttributeDefinition)
                                    {
                                        AttributeDefinition attributeDefinition = databaseObject as AttributeDefinition;

                                        if (!attributeDefinition.Constant)
                                        {
                                            using (AttributeReference attributeReference = new AttributeReference())
                                            {
                                                attributeReference.SetAttributeFromBlock(attributeDefinition, blockReference.BlockTransform);
                                                attributeReference.Position = attributeDefinition.Position.TransformBy(blockReference.BlockTransform);

                                                //attributeReference.TextString = acAtt.TextString;
                                                attributeReference.TextString = UpdateAttribute(attributeReference, attributeDefinition.TextString, tag);

                                                blockReference.AttributeCollection.AppendAttribute(attributeReference);

                                                t.AddNewlyCreatedDBObject(attributeReference, true);
                                            }
                                        }
                                    }

                                    // Visibility1 = "All text"
                                    DynamicBlockReferencePropertyCollection pc = blockReference.DynamicBlockReferencePropertyCollection;

                                    foreach (DynamicBlockReferenceProperty prop in pc)
                                    {
                                        if (prop.PropertyName.ToUpper() == "VISIBILITY1")
                                        {
                                            prop.Value = "All Text";
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // commit changes!
                    t.Commit();
                }
            }
        }


        // Update Attribute Fire Symbols
        private static string UpdateAttribute(AttributeReference acAttRef, string defaultString, FireAlarmDevice fd)
        {
            string outputString = "";
            if (acAttRef != null)
            {
                if (fd != null)
                {
                    switch (acAttRef.Tag.ToUpper())
                    {
                        case "LP":
                            outputString = fd.Loop;
                            break;
                        case "ADD":
                            outputString = fd.Address;
                            break;
                        case "ZONE":
                            outputString = fd.PriZone;
                            break;
                        case "ROOMNAME":
                            outputString = fd.LocationText;
                            break;
                        case "BLOCKREF":
                            outputString = fd.BlockReference + "." + fd.RoomNumber;
                            break;
                        default:
                            outputString = defaultString;
                            break;
                            
                    }
                }
            }
            return outputString;
        }

        // Update attribute Room Label
        private static string UpdateAttribute(AttributeReference acAttRef, string defaultString, RoomLabel label)
        {
            string outputString = "";
            if (acAttRef != null)
            {
                if (label != null)
                {
                    switch (acAttRef.Tag.ToUpper())
                    {
                        case "RNAME":
                            outputString = label.RNAME;
                            break;
                        case "RNAME2":
                            outputString = label.RNAME2;
                            break;
                        case "BLOCK":
                            outputString = label.BLOCKREF;
                            break;
                        case "ROOMNUMBER":
                            outputString = label.RoomNumber;
                            break;
                        default:
                            outputString = defaultString;
                            break;

                    }
                }
            }
            return outputString;
        }

        // Update attribute P4 Tags
        private static string UpdateAttribute(AttributeReference acAttRef, string defaultString, P4Tag tag)
        {
            string outputString = "";
            if (acAttRef != null)
            {
                if (tag != null)
                {
                    switch (acAttRef.Tag.ToUpper())
                    {
                        case "CB#":
                            outputString = tag.CollectorBoxNumber;
                            break;
                        case "NUM":
                            outputString = tag.Address;
                            break;
                        case "TYPE":
                            outputString = tag.Type;
                            break;
                        case "BLOCKREF":
                            outputString = tag.BlockRef;
                            break;
                        case "ROOMNAME":
                            outputString = tag.RoomName;
                            break;
                        default:
                            outputString = defaultString;
                            break;

                    }
                }
            }
            return outputString;
        }
    }
}
