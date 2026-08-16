/* ============================================================
   JWT Music Platform — Admin Panel shared behavior
   Drop-in, framework-free. No external dependencies besides
   Bootstrap (optional, not required for these components).
   ============================================================ */
(function () {
  "use strict";

  /* ---------- Sidebar toggle ---------- */
  function initSidebar() {
    var shell = document.querySelector(".admin-shell");
    var toggleBtns = document.querySelectorAll("[data-sidebar-toggle]");
    var backdrop = document.querySelector(".sidebar-backdrop");
    if (!shell) return;

    toggleBtns.forEach(function (btn) {
      btn.addEventListener("click", function () {
        if (window.innerWidth <= 992) {
          shell.classList.toggle("sidebar-open");
        } else {
          shell.classList.toggle("sidebar-collapsed");
        }
      });
    });
    if (backdrop) {
      backdrop.addEventListener("click", function () {
        shell.classList.remove("sidebar-open");
      });
    }
  }

  /* ---------- Modals (data-modal-target / data-modal-close) ---------- */
  function initModals() {
    document.querySelectorAll("[data-modal-open]").forEach(function (trigger) {
      trigger.addEventListener("click", function () {
        var id = trigger.getAttribute("data-modal-open");
        var modal = document.getElementById(id);
        if (modal) modal.classList.add("show");
      });
    });
    document.querySelectorAll("[data-modal-close]").forEach(function (trigger) {
      trigger.addEventListener("click", function () {
        var modal = trigger.closest(".modal-backdrop");
        if (modal) modal.classList.remove("show");
      });
    });
    document.querySelectorAll(".modal-backdrop").forEach(function (modal) {
      modal.addEventListener("click", function (e) {
        if (e.target === modal) modal.classList.remove("show");
      });
    });
    document.addEventListener("keydown", function (e) {
      if (e.key === "Escape") {
        document.querySelectorAll(".modal-backdrop.show").forEach(function (m) {
          m.classList.remove("show");
        });
      }
    });
  }

  /* ---------- Delete confirmation helper ----------
     Usage: <button data-confirm-delete data-modal-open="deleteModal"
                     data-name="Song Name" data-target-form="#deleteForm123">
     Populates [data-confirm-name] text inside the confirm modal. */
  function initConfirmDelete() {
    document.querySelectorAll("[data-confirm-delete]").forEach(function (btn) {
      btn.addEventListener("click", function () {
        var name = btn.getAttribute("data-name") || "this item";
        var modalId = btn.getAttribute("data-modal-open");
        var modal = document.getElementById(modalId);
        if (!modal) return;
        var nameEl = modal.querySelector("[data-confirm-name]");
        if (nameEl) nameEl.textContent = name;
        var confirmBtn = modal.querySelector("[data-confirm-submit]");
        var targetForm = btn.getAttribute("data-target-form");
        if (confirmBtn && targetForm) {
          confirmBtn.onclick = function () {
            var form = document.querySelector(targetForm);
            if (form) form.submit();
            modal.classList.remove("show");
          };
        }
      });
    });
  }

  /* ---------- File upload preview ----------
     Usage: wrap an <input type="file"> in .upload-zone with a sibling
     .upload-preview containing [data-file-name], [data-file-meta],
     and optionally an <img data-file-thumb>. */
  function initUploads() {
    document.querySelectorAll(".upload-zone input[type=file]").forEach(function (input) {
      var zone = input.closest(".upload-zone");
      var wrapper = zone ? zone.parentElement : null;
      var preview = wrapper ? wrapper.querySelector(".upload-preview") : null;

      input.addEventListener("change", function () {
        if (!input.files || !input.files[0] || !preview) return;
        var file = input.files[0];
        var nameEl = preview.querySelector("[data-file-name]");
        var metaEl = preview.querySelector("[data-file-meta]");
        var thumb = preview.querySelector("[data-file-thumb]");
        if (nameEl) nameEl.textContent = file.name;
        if (metaEl) metaEl.textContent = (file.size / 1024 / 1024).toFixed(2) + " MB";
        if (thumb && file.type.indexOf("image") === 0) {
          var reader = new FileReader();
          reader.onload = function (e) { thumb.src = e.target.result; thumb.style.display = "block"; };
          reader.readAsDataURL(file);
        } else if (thumb) {
          thumb.style.display = "none";
        }
        preview.classList.add("show");
      });

      ["dragover", "dragleave", "drop"].forEach(function (evt) {
        if (!zone) return;
        zone.addEventListener(evt, function (e) {
          e.preventDefault();
          zone.classList.toggle("dragover", evt === "dragover");
        });
      });
    });

    document.querySelectorAll(".remove-file").forEach(function (btn) {
      btn.addEventListener("click", function () {
        var preview = btn.closest(".upload-preview");
        var wrapper = preview ? preview.parentElement : null;
        var input = wrapper ? wrapper.querySelector("input[type=file]") : null;
        if (input) input.value = "";
        if (preview) preview.classList.remove("show");
      });
    });
  }

  /* ---------- Client-side table search / filter / sort ----------
     Usage: table wrapped with data-table-root, search input with
     data-table-search, column headers with data-sort-key matching
     td data-value on the same column. Mock-data friendly. */
  function initTables() {
    document.querySelectorAll("[data-table-root]").forEach(function (root) {
      var table = root.querySelector(".data-table");
      var tbody = table ? table.querySelector("tbody") : null;
      var searchInput = root.querySelector("[data-table-search]");
      var filterSelects = root.querySelectorAll("[data-table-filter]");

      function applyFilters() {
        if (!tbody) return;
        var term = searchInput ? searchInput.value.trim().toLowerCase() : "";
        var filters = {};
        filterSelects.forEach(function (sel) {
          var key = sel.getAttribute("data-table-filter");
          if (sel.value) filters[key] = sel.value.toLowerCase();
        });

        Array.prototype.forEach.call(tbody.rows, function (row) {
          var text = row.textContent.toLowerCase();
          var matchesSearch = !term || text.indexOf(term) !== -1;
          var matchesFilters = true;
          Object.keys(filters).forEach(function (key) {
            var cellVal = (row.getAttribute("data-" + key) || "").toLowerCase();
            if (cellVal !== filters[key]) matchesFilters = false;
          });
          row.style.display = matchesSearch && matchesFilters ? "" : "none";
        });
      }

      if (searchInput) searchInput.addEventListener("input", applyFilters);
      filterSelects.forEach(function (sel) { sel.addEventListener("change", applyFilters); });

      table && table.querySelectorAll("thead th[data-sort-key]").forEach(function (th, idx) {
        var dir = 1;
        th.addEventListener("click", function () {
          var key = th.getAttribute("data-sort-key");
          var rows = Array.prototype.slice.call(tbody.rows);
          rows.sort(function (a, b) {
            var av = a.querySelector('[data-col="' + key + '"]');
            var bv = b.querySelector('[data-col="' + key + '"]');
            av = av ? (av.getAttribute("data-value") || av.textContent).trim() : "";
            bv = bv ? (bv.getAttribute("data-value") || bv.textContent).trim() : "";
            var an = parseFloat(av), bn = parseFloat(bv);
            if (!isNaN(an) && !isNaN(bn)) return (an - bn) * dir;
            return av.localeCompare(bv) * dir;
          });
          rows.forEach(function (r) { tbody.appendChild(r); });
          table.querySelectorAll("thead .sort-arrow").forEach(function (a) { a.textContent = "↕"; });
          var arrow = th.querySelector(".sort-arrow");
          if (arrow) arrow.textContent = dir === 1 ? "↑" : "↓";
          dir *= -1;
        });
      });
    });
  }

  /* ---------- Dropdown menus (profile / notifications) ---------- */
  function initDropdowns() {
    document.querySelectorAll("[data-dropdown-toggle]").forEach(function (btn) {
      var menu = document.getElementById(btn.getAttribute("data-dropdown-toggle"));
      if (!menu) return;
      btn.addEventListener("click", function (e) {
        e.stopPropagation();
        document.querySelectorAll(".dropdown-menu.show").forEach(function (m) {
          if (m !== menu) m.classList.remove("show");
        });
        menu.classList.toggle("show");
      });
    });
    document.addEventListener("click", function () {
      document.querySelectorAll(".dropdown-menu.show").forEach(function (m) { m.classList.remove("show"); });
    });
  }

  document.addEventListener("DOMContentLoaded", function () {
    initSidebar();
    initModals();
    initConfirmDelete();
    initUploads();
    initTables();
    initDropdowns();
  });
})();
