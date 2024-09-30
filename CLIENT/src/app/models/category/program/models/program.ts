import {createDefaultModelBase, ModelBase} from "../../../base/modelBase";

export interface Program extends ModelBase {
  name: string | null;
  note: string | null;
}

export function createDefaultProgram(): Program {
  let modelBase: ModelBase = createDefaultModelBase();
  return {
    name: null,
    note: null,
    ...modelBase
  }
}
