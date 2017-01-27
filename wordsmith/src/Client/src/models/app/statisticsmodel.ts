export class StatisticsModel {
    popularSentences: string[] = [];
    recentSentences: string[] = [];
    totalSentences: number;


    static ConvertFromServiceModel(serviceObject: any): StatisticsModel {
        let model = new StatisticsModel();
        model.popularSentences = serviceObject.mostPopularSentences;
        model.recentSentences = serviceObject.mostRecentSentences;
        model.totalSentences = serviceObject.totalSentences;
        return model;
    }

}