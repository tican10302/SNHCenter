import {createDefaultDtoBase, DtoBase} from "../../../base/dtoBase";

export interface ProgramDto extends DtoBase {
  name: string | null;
  note: string | null;
}

export function createDefaultProgramDto(): ProgramDto {
  let dtoBase: DtoBase = createDefaultDtoBase();
  return {
    name: null,
    note: null,
    ...dtoBase
  }
}
