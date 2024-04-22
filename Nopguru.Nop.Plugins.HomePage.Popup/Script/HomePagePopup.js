$(document).ready(function () {

  let selectOption = $('#courses');
  $.ajax({
    type: 'GET',
    url: '/HomePagePopup/HomePagePopupFront',
    dataType: "json",
    success: function (data) {
      $.each(data, function (val, text) {
        selectOption.append(
          $('<option></option>').val(text.Value).html(text.Text)
        );
      });
    },
    failure: function () {
      alert("Failed!");
    }
  });

  $(document).on("click", "#closebtn", function () {
    $("#HomePagePopup").hide();
  });

  $(document).on("click", "#homepagepopupbtn", function () {

    let filename = window.location.pathname;
    //.split("/")
    //.filter(function (c) { return c.length; })
    //.pop();
    let phoneno = /^\d{10}$/;
    let mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

    let name = document.HomePagePopupform1.name.value;
    let contactnumber = document.HomePagePopupform1.contactnumber.value;
    let email = document.HomePagePopupform1.email.value;
    let subject = document.HomePagePopupform1.subject.value;
    let courses = document.getElementById("courses").value;
    let pageName = filename;
    let affiliateId = document.getElementById("affiliateId").value;

    if (name == null || name == "") {
      alert("Name can't be blank");
      return false;
    }
    else if (!contactnumber.match(phoneno)) {
      alert("Contact number is not valid");
      return false;
    }
    else if (!email.match(mailformat)) {
      alert("Email is not valid");
      return false;
    }
    else if (subject == null || subject == "") {
      alert("Subject can't be blank");
      return false;
    }
    else if (courses == "0") {
      alert('Please select one course name');
      return false;
    }
    else {

      let postData = {
        name: $("#name").val(),
        contactnumber: $("#contactnumber").val(),
        email: $("#email").val(),
        subject: $("#subject").val(),
        courses: $("#courses").val(),
        pageName: pageName,
        affiliateId: affiliateId
      };
      addAntiForgeryToken(postData);

      $.ajax({
        cache: false,
        type: 'POST',
        url: '/HomePagePopup/HomePagePopupFront/',
        data: postData,
        success: function (data, textStatus, jqXHR) {
          if (data.Result) {
            $("#HomePagePopup").hide();
            alert("Hey !! You are lucky & you will soon receive a coupon code.");
          }
        }
      });
    }
  });
});