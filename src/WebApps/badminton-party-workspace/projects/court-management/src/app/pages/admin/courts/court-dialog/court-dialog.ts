import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { BadmintonCourt } from '../courts';

@Component({
    selector: 'app-court-dialog',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatDialogModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatSelectModule
    ],
    templateUrl: './court-dialog.html',
    styles: [`
    .court-form { padding-top: 8px; display: flex; flex-direction: column; gap: 8px; min-width: 450px; }
    .full-width { width: 100%; }
    .form-row { display: grid; grid-template-columns: 1fr 1fr; gap: 16px; }
  `]
})
export class CourtDialogComponent {
    form: FormGroup;
    isEdit: boolean;
    centers: any[];

    constructor(
        private fb: FormBuilder,
        public dialogRef: MatDialogRef<CourtDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { court?: BadmintonCourt, centers: any[] }
    ) {
        this.isEdit = !!data.court;
        this.centers = data.centers;

        this.form = this.fb.group({
            centerId: [data.court?.centerId || '', [Validators.required]],
            name: [data.court?.name || '', [Validators.required]],
            description: [data.court?.description || ''],
            normalFee: [data.court?.normalFee || ''],
            peakFee: [data.court?.peakFee || ''],
            disabledPeriods: [data.court?.disabledPeriods || ''],
            status: [data.court?.status || 'available', [Validators.required]]
        });
    }

    onSave() {
        if (this.form.valid) {
            const selectedCenter = this.centers.find(c => c.id === this.form.value.centerId);
            this.dialogRef.close({
                ...this.data.court,
                ...this.form.value,
                centerName: selectedCenter?.name
            });
        }
    }

    onCancel() {
        this.dialogRef.close();
    }
}
