﻿using System.Collections.Generic;
using CoreNodeModels;
using Dynamo.Graph.Nodes;
using Dynamo.Utilities;
using ProtoCore.AST.AssociativeAST;
using Newtonsoft.Json;

namespace AdvanceSteel.Nodes
{
	[NodeName("Beam Ortho Notch Feature Properties")]
	[NodeDescription("Select Advance Steel Beam Ortho Notch Feature Property Type to Set")]
  [NodeCategory("AdvanceSteel.Nodes.Properties.Properties-Write")]
  [OutPortNames("Writable Beam Ortho Notch Feature Property")]
  [OutPortTypes("string")]
  [OutPortDescriptions("string")]
  [IsDesignScriptCompatible]
	public class ASPropertiesBeamNotchSqFeatures : AstDropDownBase
	{
		private const string outputName = "Advance Steel Beam Ortho Notch Feature Property";

    public ASPropertiesBeamNotchSqFeatures()
				: base(outputName)
		{
			InPorts.Clear();
			OutPorts.Clear();
			RegisterAllPorts();
		}

		[JsonConstructor]
		public ASPropertiesBeamNotchSqFeatures(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts)
		: base(outputName, inPorts, outPorts)
		{
		}

		protected override SelectionState PopulateItemsCore(string currentSelection)
		{
			Items.Clear();

      var newItems = new List<DynamoDropDownItem>() { };
      foreach (var item in Utils.GetBeamNotchOrthoPropertyList(ePropertyDataOperator.Set))
      {
        newItems.Add(new DynamoDropDownItem(item.Key, item.Value));
      }
			Items.AddRange(newItems);

			SelectedIndex = 0;
			return SelectionState.Restore;
		}

		public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
		{
      if (Items.Count == 0 ||
          Items[SelectedIndex].Name == "None" ||
          SelectedIndex < 0)
      {
        return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), AstFactory.BuildNullNode()) };
      }

      var intNode = AstFactory.BuildPrimitiveNodeFromObject((string)Items[SelectedIndex].Name);
      var assign = AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), intNode);

      return new List<AssociativeNode> { assign };

    }
	}
}
