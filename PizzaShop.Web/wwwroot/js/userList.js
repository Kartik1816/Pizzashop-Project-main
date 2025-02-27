document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.getElementById('searchInput');
    const tableBody = document.getElementById('tableBody');
    let timeoutId;
  
    searchInput.addEventListener('input', function () {
      clearTimeout(timeoutId);
  
      // Add small delay to prevent too many requests
      timeoutId = setTimeout(() => {
        const searchTerm = searchInput.value;
  
        fetch(`/UserList/SearchUsers?searchTerm=${encodeURIComponent(searchTerm)}`)
          .then(response => response.json())
          .then(data => {
            tableBody.innerHTML = ''; // Clear current rows
            console.log(data);
            data.forEach(user => {
              const row = `
                <tr>
                  <td>${user.requestedUsername}</td>
                  <td>${user.email}</td>
                  <td>${user.phone}</td>
                  <td>${user.roleName}</td>
                  <td>
                    ${user.status == 1 ?
                      `<span class="status-badge">Active</span>` :
                      `<span class="text-danger">Inactive</span>`
                    }
                  </td>
                  <td><a onclick="editUser(${user.userId})"><button class="btn btn-light btn-sm">
                    <i class="fas fa-pencil"></i></button></a>
                <button type="button" class="btn-light btn btn-sm delete-icon-border" data-bs-toggle="modal"  data-bs-target="#deleteModel" onclick="openModel(${user.userId}">
                                        <a href="#" class="delete-icon">  <i class="fas fa-trash"></i></a></button>
              </td>
                </tr>
              `;
              tableBody.innerHTML += row;
            });
          })
          .catch(error => console.error('Error:', error));
      }, 300); // 300ms delay
    });
  });

function editUser (userId) {
  window.location.href = '/UserList/EditUser/' + userId;
}
function openModel(id) {
  console.log(id);
  $("#deleteButton").click(function () {
      $.ajax({
          type: "DELETE",
          url: "/UserList/DeleteUser/" + id,
          data: { id: id },
          success: function (data) {
              if (data.success) {
                  toastr.success(data.message);
                  setTimeout(function () {
                      location.reload();
                  }, 1000);
              } else {
                  toastr.error(data.message);
              }
          },
          error: function () {
              toastr.error("Error");
          }
      });
  });
}
