import { createAction, props } from "@ngrx/store";
import { LineGraphData } from "../../models/line-graph-data";

export const loadLast12MonthsBalancesAction = createAction(
    '[Last12MonthGraphComponent] On Component Load',
    props<{ userId: string }>()
  );

  export const last12MonthsBalancesLoadedAction = createAction(
    '[DashBoard Effect] Last 12 Month Balances Loaded',
    props<{ lineGraphData: LineGraphData }>(),
  );

  export const last12MonthsBalancesLoadErrorAction = createAction(
    '[DashBoard Effect] Last 12 Month Balances Load Error'
  );