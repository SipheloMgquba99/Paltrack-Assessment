import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { LogisticsService } from '../../shared/services/logistics.service';
import {
    DxDataGridComponent,
    DxDataGridModule,
    DxButtonModule,
} from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store';
import ExcelJS from 'exceljs';
import { saveAs } from 'file-saver';

@Component({
    selector: 'app-logistics-table',
    templateUrl: './logistics-table.html',
    styleUrls: ['./logistics-table.css'],
    standalone: true,
    imports: [DxDataGridModule, DxButtonModule],
})
export class LogisticsTableComponent implements OnInit {
    @ViewChild(DxDataGridComponent, { static: false })
    dataGrid!: DxDataGridComponent;

    pageSize = 10;
    dataSource: any;
    loading = true;
    paginationSummary = '';

    constructor(
        private logisticsService: LogisticsService,
        private cd: ChangeDetectorRef
    ) {}

    ngOnInit(): void {
        this.logisticsService.getAllLogisticsPartners().subscribe({
            next: (response) => {
                console.log('API Response:', response);

                const logisticsData = Array.isArray(response?.data)
                    ? response.data
                    : [];

                this.dataSource = new ArrayStore({
                    data: logisticsData,
                    key: 'id',
                });

                this.loading = false;
                this.updatePaginationSummary();
                this.cd.detectChanges();
            },
            error: (err) => {
                console.error('Failed to load logistics partners', err);
                this.loading = false;
            },
        });
    }

    onContentReady(): void {
        setTimeout(() => this.updatePaginationSummary());
    }

    onOptionChanged(e: any): void {
        if (
            e.fullName === 'paging.pageIndex' ||
            e.fullName === 'paging.pageSize'
        ) {
            setTimeout(() => this.updatePaginationSummary());
        }
    }

    updatePaginationSummary(): void {
        if (!this.dataGrid) {
            this.paginationSummary = '';
            return;
        }

        const instance = this.dataGrid.instance;
        const pageIndex = instance.pageIndex();
        const pageSize = instance.pageSize();

        const totalCount =
            instance.totalCount() >= 0
                ? instance.totalCount()
                : instance.getDataSource().items().length;

        const start = totalCount === 0 ? 0 : pageIndex * pageSize + 1;
        const end = Math.min(start + pageSize - 1, totalCount);

        this.paginationSummary = `Showing ${start} to ${end} of ${totalCount} entries`;
    }

    exportGrid(): void {
        import('devextreme/excel_exporter').then(({ exportDataGrid }) => {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet('Logistics');

            exportDataGrid({
                component: this.dataGrid.instance,
                worksheet: worksheet,
                autoFilterEnabled: true,
            }).then(() => {
                workbook.xlsx.writeBuffer().then((buffer: ArrayBuffer) => {
                    const blob = new Blob([buffer], {
                        type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
                    });
                    saveAs(blob, 'Logistics.xlsx');
                });
            });
        });
    }
}
