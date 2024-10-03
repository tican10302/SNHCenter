import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {DialogModule} from "primeng/dialog";
import {ButtonModule} from "primeng/button";
import {createDefaultProgramForm} from "../../../../models/category/program/programModel";
import {FormsModule} from "@angular/forms";
import {InputTextModule} from "primeng/inputtext";
import {NgIf} from "@angular/common";
import {Enum} from "../../../../enums/enum";
import {ProgramService} from "../../../../services/category/program.service";
import {MessageService} from "primeng/api";

@Component({
  selector: 'app-program-view',
  standalone: true,
  imports: [
    DialogModule,
    ButtonModule,
    FormsModule,
    InputTextModule,
    NgIf
  ],
  templateUrl: './program-view.component.html',
  styleUrl: './program-view.component.scss'
})
export class ProgramViewComponent implements OnInit{
  @Input() visible!: boolean;
  @Input() isView!: boolean;
  @Input() isEdit!: boolean;

  @Output() onCancel: EventEmitter<void> = new EventEmitter();

  model: any;

  constructor(private programService: ProgramService, private messageService: MessageService) {
  }

  ngOnInit() {
    this.model = createDefaultProgramForm();
  }

  saveData() {
    if(this.isEdit)
      this.programService.updateData(this.model);
    else
      this.programService.addData(this.model).subscribe({
        next: (data) => {
          if(data)
          {
            this.messageService.add({severity: 'success', summary: 'Success', detail: `Create data successfully`, life: Enum.messageLife});
          }
        },
        error: (err) =>  this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
      });
    this.isView = false;
    this.isEdit = false;
    this.visible = false;
  }

  cancel() {
    this.onCancel.emit();
  }
}
