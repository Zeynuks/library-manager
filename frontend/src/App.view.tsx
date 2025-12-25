import {Navigate, Route, Routes} from "react-router-dom";
import {LoginPage} from "@/view/pages/LoginPage.tsx";
import {ProfilePage} from "@/view/pages/ProfilePage.tsx";

import {CreateBookPage} from "@/view/pages/CreateBookPage.tsx";
import {BookListPage} from "@/view/pages/BookListPage.tsx";
import {BookPage} from "@/view/pages/BookPage.tsx";

import {CreateFinePage} from "@/view/pages/CreateFinePage.tsx";
import {FinePage} from "@/view/pages/FinePage.tsx";
import {FineListPage} from "@/view/pages/FineListPage.tsx";

import {ReaderCategoryListPage} from "@/view/pages/ReaderCategoryListPage.tsx";
import {ReaderCategoryPage} from "@/view/pages/ReaderCategoryPage.tsx";
import {CreateReaderCategoryPage} from "@/view/pages/CreateReaderCategoryPage.tsx";

import {TariffListPage} from "@/view/pages/TariffListPage.tsx";
import {CreateTariffPage} from "@/view/pages/CreateTariffPage.tsx";
import {TariffPage} from "@/view/pages/TariffPage.tsx";

import {ReaderListPage} from "@/view/pages/ReaderListPage.tsx";
import {CreateReaderPage} from "@/view/pages/CreateReaderPage.tsx";
import {ReaderPage} from "@/view/pages/ReaderPage.tsx";
import {RentalListPage} from "@/view/pages/RentalListPage.tsx";
import {CreateRentalPage} from "@/view/pages/CreateRentalPage.tsx";
import {RentalPage} from "@/view/pages/RentalPage.tsx";
import {ProtectedRoute} from "@/ProtectedRoute.tsx";
import {ForbiddenPage} from "@/view/pages/ForbiddenPage.tsx";
import {UserListPage} from "@/view/pages/UserListPage.tsx";
import {CreateUserPage} from "@/view/pages/CreateUserPage.tsx";
import {UserPage} from "@/view/pages/UserPage.tsx";
import {RentalReturnPage} from "@/view/pages/RentalReturnPage.tsx";

export const AppView = () => (
    <Routes>
        <Route path="/" element={<Navigate to="/profile" replace />} />
        <Route path="/login" element={<LoginPage/>}/>
        <Route path="/profile" element={<ProfilePage/>}/>
        <Route path="/403" element={<ForbiddenPage/>}/>

        <Route element={<ProtectedRoute allowedRoles={["Manager"]}/>}>
            <Route path="/books/create" element={<CreateBookPage/>}/>
            <Route path="/tariffs/create" element={<CreateTariffPage/>}/>
            <Route path="/fines/create" element={<CreateFinePage/>}/>
            <Route path="/reader-categories/create" element={<CreateReaderCategoryPage/>}/>
            <Route path="/readers/create" element={<CreateReaderPage/>}/>
        </Route>

        <Route element={<ProtectedRoute allowedRoles={["Operator"]}/>}>
            <Route path="/rentals/create" element={<CreateRentalPage/>}/>
        </Route>

        <Route element={<ProtectedRoute allowedRoles={["Manager", "Operator", "Administrator"]}/>}>
            <Route path="/books" element={<BookListPage/>}/>
            <Route path="/books/:id" element={<BookPage/>}/>
            <Route path="/fines" element={<FineListPage/>}/>
            <Route path="/fines/:id" element={<FinePage/>}/>
            <Route path="/reader-categories" element={<ReaderCategoryListPage/>}/>
            <Route path="/reader-categories/:id" element={<ReaderCategoryPage/>}/>
            <Route path="/readers" element={<ReaderListPage/>}/>
            <Route path="/readers/:id" element={<ReaderPage/>}/>
            <Route path="/tariffs" element={<TariffListPage/>}/>
            <Route path="/tariffs/:id" element={<TariffPage/>}/>
            <Route path="/rentals" element={<RentalListPage/>}/>
            <Route path="/rentals/:id" element={<RentalPage/>}/>
            <Route path="/rentals/:id/return" element={<RentalReturnPage />} />
        </Route>

        <Route element={<ProtectedRoute allowedRoles={["Administrator"]}/>}>
            <Route path="/users/" element={<UserListPage/>}/>
            <Route path="/users/create" element={<CreateUserPage/>}/>
            <Route path="/users/:id" element={<UserPage/>}/>
        </Route>

        <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>

);
