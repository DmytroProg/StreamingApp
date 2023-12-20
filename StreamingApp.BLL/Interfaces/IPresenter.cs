using StreamingApp.BLL.Responses;

namespace StreamingApp.BLL.Interfaces;

public interface IPresenter
{
    void ChangeView(ResponseBase response);
}
