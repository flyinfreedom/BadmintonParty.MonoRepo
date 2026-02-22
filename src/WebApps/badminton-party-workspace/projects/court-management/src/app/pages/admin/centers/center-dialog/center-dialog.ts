import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { BadmintonCenter } from '../centers';

@Component({
    selector: 'app-center-dialog',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatDialogModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule
    ],
    templateUrl: './center-dialog.html',
    styleUrls: ['./center-dialog.scss']
})
export class CenterDialogComponent {
    form: FormGroup;
    isEdit: boolean;

    constructor(
        private fb: FormBuilder,
        public dialogRef: MatDialogRef<CenterDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { center?: BadmintonCenter }
    ) {
        this.isEdit = !!data.center;
        this.form = this.fb.group({
            name: [data.center?.name || '', [Validators.required]],
            description: [data.center?.description || ''],
            owner: [data.center?.owner || ''],
            address: [data.center?.address || ''],
            phone: [data.center?.phone || ''],
            lineAccount: [data.center?.lineAccount || '']
        });
    }

    onSave() {
        if (this.form.valid) {
            this.dialogRef.close({ ...this.data.center, ...this.form.value });
        }
    }

    onCancel() {
        this.dialogRef.close();
    }
}
