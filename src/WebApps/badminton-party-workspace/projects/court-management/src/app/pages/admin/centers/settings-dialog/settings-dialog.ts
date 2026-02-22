import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTabsModule } from '@angular/material/tabs';
import { MatChipsModule } from '@angular/material/chips';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { BadmintonCenter, CenterSettings } from '../centers';

@Component({
    selector: 'app-center-settings-dialog',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatDialogModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatTabsModule,
        MatChipsModule,
        MatCheckboxModule,
        MatIconModule
    ],
    templateUrl: './settings-dialog.html',
    styleUrls: ['./settings-dialog.scss']
})
export class CenterSettingsDialogComponent {
    form: FormGroup;
    hours = Array.from({ length: 24 }, (_, i) => i);
    days = [
        { value: 1, label: '週一' },
        { value: 2, label: '週二' },
        { value: 3, label: '週三' },
        { value: 4, label: '週四' },
        { value: 5, label: '週五' },
        { value: 6, label: '週六' },
        { value: 0, label: '週日' }
    ];

    unavailableHours: number[] = [];
    peakSettings: { [key: number]: number[] } = {};

    constructor(
        private fb: FormBuilder,
        public dialogRef: MatDialogRef<CenterSettingsDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { center: BadmintonCenter }
    ) {
        const s = data.center.settings;

        // 初始化尖峰時段預設值 (週六週日全天)
        const defaultPeak = [0, 6];
        this.days.forEach(d => {
            this.peakSettings[d.value] = [];
        });

        if (s) {
            this.unavailableHours = [...(s.unavailableHours || [])];
            s.peakSettings.forEach(ps => {
                this.peakSettings[ps.dayOfWeek] = [...ps.peakHours];
            });
        } else {
            // 預設週六週日為尖峰 (全天)
            this.peakSettings[6] = [...this.hours];
            this.peakSettings[0] = [...this.hours];
        }

        this.form = this.fb.group({
            bookingRangeDays: [s?.bookingRangeDays || null],
            normalFee: [s?.normalFee || null],
            peakFee: [s?.peakFee || null]
        });
    }

    toggleUnavailable(hour: number) {
        const idx = this.unavailableHours.indexOf(hour);
        if (idx > -1) {
            this.unavailableHours.splice(idx, 1);
        } else {
            this.unavailableHours.push(hour);
        }
    }

    isUnavailable(hour: number): boolean {
        return this.unavailableHours.includes(hour);
    }

    togglePeak(dayOfWeek: number, hour: number) {
        const idx = this.peakSettings[dayOfWeek].indexOf(hour);
        if (idx > -1) {
            this.peakSettings[dayOfWeek].splice(idx, 1);
        } else {
            this.peakSettings[dayOfWeek].push(hour);
        }
    }

    isPeak(dayOfWeek: number, hour: number): boolean {
        return this.peakSettings[dayOfWeek].includes(hour);
    }

    onSave() {
        const peakSettingsArray = Object.keys(this.peakSettings).map(key => ({
            dayOfWeek: parseInt(key),
            peakHours: this.peakSettings[parseInt(key)]
        }));

        const settings: CenterSettings = {
            ...this.form.value,
            unavailableHours: this.unavailableHours,
            peakSettings: peakSettingsArray
        };

        this.dialogRef.close(settings);
    }

    onCancel() {
        this.dialogRef.close();
    }
}
