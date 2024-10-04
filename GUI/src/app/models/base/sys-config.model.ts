export interface SelectListItem {
  text: string;
  value: any;
  selected?: boolean; // optional
}

export class SysConfig {
  static IsActive: SelectListItem[] = [
    { text: 'Online', value: true, selected: true },
    { text: 'Offline', value: false }
  ];

  static Gender: SelectListItem[] = [
    { text: 'Male', value: '0' },
    { text: 'Female', value: '1' },
    { text: 'Other', value: '2' }
  ];
}
