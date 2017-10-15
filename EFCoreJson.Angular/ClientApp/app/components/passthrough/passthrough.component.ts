import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'passthrough',
    templateUrl: './passthrough.component.html'
})
export class PassThroughComponent {
    public formModel: FormModel;
    public formState: FormState;
    id: number;
    private sub: any;

    constructor(private http: Http,
        @Inject('BASE_URL') private baseUrl: string) {
        this.formState = new FormState();
        this.formModel = new FormModel();
    }

    public onSubmit() {
        let submission = new PostModel(this.formModel);

        this.http.post(this.baseUrl + 'api/JsonPassThrough', submission).subscribe(result => {
            this.formState.isSubmitted = true;
        }, error => {
            this.formState.isErrorState = true;
            console.error(error);
        });
    }

}

class FormModel {
    constructor(
        public employeeId: string = "",
        public firstName: string = "",
        public lastName: string = "",
        public startDate?: number,
        public terminatedOn?: number,
        public terminationReason: string = ""
    ) { }
}

class PostModel {
    public schemaVersion: number;
    public serializedData: string;

    constructor(formData: any) {
        this.schemaVersion = 1.0;
        this.serializedData = JSON.stringify(formData);
    }
}

class FormState {

    constructor(public isSubmitted: boolean = false,
                public isErrorState: boolean = false) {
    }
}

