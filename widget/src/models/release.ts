export default class Release {
    constructor (
        id: number,
        passedTests: number,
        totalTests: number,
        releaseName: string,
        releaseId: number,
        downloadLink: string,
        tests: any [],
        success: boolean,
        state: string,
        completedDate: Date,
        runBy: string
    ) {}   
}