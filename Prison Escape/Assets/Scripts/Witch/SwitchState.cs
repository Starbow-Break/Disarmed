public enum SwitchState
{
    Nodata,     // 솥에 데이터가 들어오지 않았을 경우
    Success,    // 솥에서 제약에 성공했을 경우
    Failed,     // 솥에서 제약에 실패했을 경우
    Again       // 대사 실행 중에 다시 클릭할 경우
}